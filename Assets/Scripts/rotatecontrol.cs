//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class rotatecontrol : MonoBehaviour
//{
//    public GameObject rotateObject1, rotateObject2, rotateObject3, rotateObject4;
//    public float rotationSpeed = 30f;

//    private Rotate rotateScript1, rotateScript2, rotateScript3, rotateScript4;

//    void Start()
//    {
//        // Get Rotate scripts from each object
//        rotateScript1 = rotateObject1.GetComponent<Rotate>();
//        rotateScript2 = rotateObject2.GetComponent<Rotate>();
//        rotateScript3 = rotateObject3.GetComponent<Rotate>();
//        rotateScript4 = rotateObject4.GetComponent<Rotate>();
//    }

//    public void OnCharacterSelected(string selectedCharacter)
//    {
//        // Stop all rotations initially
//        rotateScript1.StopRotation();
//        rotateScript2.StopRotation();
//        rotateScript3.StopRotation();
//        rotateScript4.StopRotation();

//        // Start the rotation for the correct character based on selection
//        if (selectedCharacter == "CharacterOne")
//        {
//            rotateScript1.StartRotation(rotationSpeed);
//        }
//        else if (selectedCharacter == "CharacterTwo")
//        {
//            rotateScript2.StartRotation(rotationSpeed);
//        }
//        else if (selectedCharacter == "CharacterThree")
//        {
//            rotateScript3.StartRotation(rotationSpeed);
//        }
//        else if (selectedCharacter == "CharacterFour")
//        {
//            rotateScript4.StartRotation(rotationSpeed);
//        }
//    }
//}
