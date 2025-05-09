using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MegamanEXEMod.Modules
{
    public class ArmHelper
    {

        public static void ArmChanger(int Index)
        {

            GameObject.Find("EXEBuster").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("EXEBuster").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("EXESword").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("EXESwordDark").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("EXESwordRed").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("GutsPunch").GetComponent<MeshRenderer>().enabled = false;

            switch (Index)
            {
                case 0:
                    break;
                case 1:
                    GameObject.Find("EXEBuster").transform.localScale = new Vector3(1, 1, 1);
                    GameObject.Find("EXEBuster").GetComponent<MeshRenderer>().enabled = true;
                    break;
                case 2:
                    GameObject.Find("EXESword").GetComponent<MeshRenderer>().enabled = true;
                    break;
                case 3:
                    GameObject.Find("EXESwordRed").GetComponent<MeshRenderer>().enabled = true;
                    break;
                case 4:
                    GameObject.Find("EXESwordDark").GetComponent<MeshRenderer>().enabled = true;
                    break;
                case 5:
                    GameObject.Find("GutsPunch").GetComponent<MeshRenderer>().enabled = true;
                    break;
            }

        }


    }
}
