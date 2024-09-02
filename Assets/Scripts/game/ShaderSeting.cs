using UnityEngine;

public class ShaderSeting : MonoBehaviour {

	[Header("Curved materials list:")]
	public Material[] blocksMaterialList;
	
	[Header("Curved range:")]
	[Range(5,35)]
	public float moveX;
	[Range(1,30)]
	public float moveY;
	[Range(1,100)]
	public float distance;
	bool ok,ok2;
	float xPos,yPos,speedMove;
	public Color fogColor;

	void Start(){
		speedMove = Controller.speed;
	}

	void Update()
	{
		if (!GameControll.pause && !Controller.iDie) 
		{
			if (ok) 
			{
				xPos = Mathf.Lerp (xPos, moveX, Time.deltaTime * speedMove/85);
				if (xPos >= moveX - 0.01f)
					ok = !ok;
			} else 
			{
				xPos = Mathf.Lerp (xPos, -moveX, Time.deltaTime *  speedMove/100);
				if (xPos <= -moveX + 0.01f)
					ok = !ok;
			}
			
			if (ok2) 
			{
				yPos = Mathf.Lerp (yPos, moveY, Time.deltaTime *  speedMove/35);
				if (yPos >= moveY - 0.01f)
					ok2 = !ok2;
			} else 
			{
				yPos = Mathf.Lerp (yPos, -moveY / 3, Time.deltaTime *  speedMove/45);
				if (yPos <= -moveY / 3 + 0.01f)
					ok2 = !ok2;
			}

			for (int i=0; i<blocksMaterialList.Length; i++) 
			{
				blocksMaterialList [i].SetFloat ("_Dist", distance);
				blocksMaterialList [i].SetVector ("_QOffset", new Vector4 (xPos, yPos, 0, 0));
				blocksMaterialList [i].SetColor("_Color", fogColor);
			}
		}

	}

}
