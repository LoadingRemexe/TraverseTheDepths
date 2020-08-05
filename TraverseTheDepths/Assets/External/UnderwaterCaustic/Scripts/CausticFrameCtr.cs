using UnityEngine;
using System.Collections;

public class CausticFrameCtr : MonoBehaviour {
	public Projector m_projector;
	public float causticSpeed = 1.0f;
	private int frameCount=0;
	private int frameIndex=0;
	private int mask=0;
	private int row=0;
	private int col=0;
	private Vector4 CausticFrameVar=Vector4.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void CalculateFrameCount()
	{
		frameCount=(int)(Time.frameCount*causticSpeed);
		frameIndex = imod(frameCount, 48);
		mask = frameIndex / 16;
		row =  imod(frameIndex, 16) / 4;
		col =  imod(imod(frameIndex, 16), 4);
		CausticFrameVar = new Vector4(mask, 0.25f*col, 0.25f*row, 0);
	}
	int imod(int x, int f)
	{
		return x-x/f*f;
	}

	Matrix4x4 CalculateProjectorMatrix(Projector p)
	{
		Matrix4x4 v2w = Matrix4x4.TRS(p.transform.position, p.transform.rotation, p.transform.localScale);
		Matrix4x4 w2v = Matrix4x4.Inverse (v2w);
		
		Matrix4x4 perspective = Matrix4x4.Perspective (p.fieldOfView, p.aspectRatio, p.nearClipPlane, p.farClipPlane);
		
		return perspective*w2v;
	}
	void OnPreRender()
	{
		if (m_projector != null) 
		{
			if(!m_projector.orthographic)
			{
				Shader.SetGlobalMatrix ("_MyProjector", CalculateProjectorMatrix (m_projector));
			}
		}
		CalculateFrameCount();
		Shader.SetGlobalVector("_CausticFrameVar", CausticFrameVar);
	}
}
