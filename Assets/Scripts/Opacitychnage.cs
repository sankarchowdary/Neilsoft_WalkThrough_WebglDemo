using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opacitychnage : MonoBehaviour
{
    // Start is called before the first frame update
        public GameObject currentGameObject, currentGameObject1, currentGameObject2, currentGameObject3;
        // private AsistantControll AsistantControllScript;
        public float alpha = 0.5f;//half transparency
                                  //Get current material
        private Material currentMat,currentMat1,currentMat2,currentMat3;

        // Start is called before the first frame update
        void Start()
        {
            // AsistantControllScript = FindObjectOfType<AsistantControll>();

            currentMat = currentGameObject.GetComponent<Renderer>().material;
            currentMat1 = currentGameObject1.GetComponent<Renderer>().material;
            currentMat2 = currentGameObject2.GetComponent<Renderer>().material;
            currentMat3 = currentGameObject3.GetComponent<Renderer>().material;

    }

        // Update is called once per frame
        void Update()
        {

        }



        void ChangeAlpha(Material mat, float alphaVal)
        {
            Color oldColor = mat.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
            mat.SetColor("_Color", newColor);

        }

        public void ChangeAlphaOnValue(Slider slider)
        {
            ChangeAlpha(currentMat, slider.value);
            ChangeAlpha(currentMat1, slider.value);
            ChangeAlpha(currentMat2, slider.value);
            ChangeAlpha(currentMat3, slider.value);
    }




    }
