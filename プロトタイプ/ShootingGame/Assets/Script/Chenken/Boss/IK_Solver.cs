using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEditor;

public class E_IKSolver : Editor
{
	public override void OnInspectorGUI()
	{
		IK_Solver solver = (IK_Solver)target;
		//
		if(solver.needResetOption)
		{
			GUI.enabled = false;
		}
		DrawDefaultInspector();
		if(solver.needResetOption)
		{
			GUI.enabled = true;
			if(GUILayout.Button("Reset"))
			{
				//
				solver.ResetHierarchy();
			}
		}
	}
}

[ExecuteInEditMode]
public class IK_Solver : MonoBehaviour
{
    [Serializable]
	public class Bone
	{
		public Transform boneTransform;
		[HideInInspector]
		public float length;
		[HideInInspector]
		public Vector3 originPos;
		[HideInInspector]
		public Vector3 originScale;
		[HideInInspector]
		public Quaternion originRotation;
	}

	public Bone[] bones;
	public Transform endPointOfLastBone;
	public Transform poleTarget;
	public int iterations;

	[Header("EditorMode")]
	public bool enable;

	public bool needResetOption = false;

	private Vector3 lastTargetPos;
	private bool editorInitialized = false;

	private void Start()
	{
		lastTargetPos = transform.position;
		if(Application.isPlaying && !editorInitialized)
		{
			Initialize();
		}
	}

	private void Update()
	{
		if(Application.isEditor && enable && !editorInitialized)
		{
			if(enable)
			{
				if(bones.Length == 0)
				{
					enable = false;
					return;
				}
				for(var i = 0;i < bones.Length; ++i)
				{
					if(bones[i].boneTransform == null)
					{
						enable = false;
						return;
					}
				}
				if(endPointOfLastBone == null)
				{
					enable = false;
					return;
				}
				if(poleTarget == null)
				{
					enable = false;
					return;
				}
			}
			Initialize();
		}
		if(lastTargetPos != transform.position)
		{
			if(Application.isPlaying || (Application.isEditor && enable))
			{
				Solve();
			}
		}
	}

	private void Initialize()
	{
		bones[0].originPos = bones[0].boneTransform.position;
		bones[0].originScale = bones[0].boneTransform.localScale;
		bones[0].originRotation = bones[0].boneTransform.rotation;
		bones[0].length = Vector3.Distance(endPointOfLastBone.position, bones[0].boneTransform.position);

		GameObject go = new GameObject();
		go.name = bones[0].boneTransform.name;
		go.transform.position = bones[0].boneTransform.position;
		go.transform.up = -(endPointOfLastBone.position - bones[0].boneTransform.position);
		go.transform.parent = bones[0].boneTransform.parent;
		bones[0].boneTransform.parent = go.transform;
		bones[0].boneTransform = go.transform;
		for(var i = 0; i < bones.Length; ++i)
		{
			bones[i].originPos = bones[i].boneTransform.position;
			bones[i].originScale = bones[i].boneTransform.localScale;
			bones[i].originRotation = bones[i].boneTransform.rotation;
			bones[i].length = Vector3.Distance(bones[i - 1].boneTransform.position, bones[i].boneTransform.position);
			go = new GameObject();
			go.name = bones[i].boneTransform.name;
			go.transform.position = bones[i].boneTransform.position;
			go.transform.up = -(bones[i - 1].boneTransform.position - bones[i].boneTransform.position);
			go.transform.parent = bones[i].boneTransform.parent;
			bones[i].boneTransform.parent = go.transform;
			bones[i].boneTransform = go.transform;
		}
		editorInitialized = true;
		needResetOption = true;
	}

	private void Solve()
	{
		Vector3 rootPoint = bones[bones.Length - 1].boneTransform.position;
		bones[bones.Length - 1].boneTransform.up = -(poleTarget.position - bones[bones.Length - 1].boneTransform.position);
		for(var i = bones.Length - 2; i >= 0; --i)
		{
			bones[i].boneTransform.position = bones[i + 1].boneTransform.position + (-bones[i + 1].boneTransform.up * bones[i + 1].length);
			bones[i].boneTransform.up = -(poleTarget.position - bones[i].boneTransform.position);
		}

		for(var i = 0; i < iterations; ++i)
		{
			bones[0].boneTransform.up = -(transform.position - bones[0].boneTransform.position);
			bones[0].boneTransform.position = transform.position - (-bones[0].boneTransform.up * bones[0].length);
			for(var j = 0; j < bones.Length; ++j)
			{
				bones[j].boneTransform.up = -(bones[j - 1].boneTransform.position - bones[j].boneTransform.position);
				bones[j].boneTransform.position = bones[j - 1].boneTransform.position - (-bones[j].boneTransform.up * bones[j].length);
			}

			bones[bones.Length - 1].boneTransform.position = rootPoint;
			for(var j =  bones.Length - 2; j >= 0; --j)
			{
				bones[j].boneTransform.position = bones[j + 1].boneTransform.position + (-bones[j + 1].boneTransform.up * bones[j + 1].length);
			}
		}
		lastTargetPos = transform.position;
	}

	public void ResetHierarchy()
	{
		for(var i = 0; i < bones.Length; ++i)
		{
			Transform transform = bones[i].boneTransform.GetChild(0);
			bones[i].boneTransform.GetChild(0).parent = bones[i].boneTransform.parent;
			if(Application.isPlaying)
			{
				Destroy(bones[i].boneTransform.gameObject);
			}
			else
			{
				DestroyImmediate(bones[i].boneTransform.gameObject);
			}
			bones[i].boneTransform = transform;
			transform.position = bones[i].originPos;
			transform.rotation = bones[i].originRotation;
			transform.localScale = bones[i].originScale;
		}
		lastTargetPos = Vector3.zero;
		enable = false;
		editorInitialized = false;
		needResetOption = false;
	}
}
