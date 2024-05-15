using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class decoder : MonoBehaviour
{
    int countIn;
    int countOut;

    List<bool> input;
    List<bool> output;

    public InputField count;
    public InputField comb;

    public Text combOutText;
    public Text combInText;

    public Text countOutText;

    public Image image;

    public GameObject IGO;
    public GameObject OGO;
    private List<GameObject> inputsGO;
    private List<GameObject> outputsGO;
    public List<Toggle> checkb;

    string coords = "";

    public void UpdateText()
    {
        combInText.text = "";
        for(int i = 0; i < input.Count; i++)
        {
            if (input[i] == false)
                combInText.text += '0';
            else
                combInText.text += '1';
        }
        combOutText.text = "";
        for (int i = 0; i < output.Count; i++)
        {
            if (output[i] == false)
                combOutText.text += "0";
            else
                combOutText.text += "1";
        }
    }
    
    void Start()
    {
        inputsGO = new List<GameObject>();
        outputsGO = new List<GameObject>();
        checkb = new List<Toggle>();
    }

    void Start1()
    {
        countOut = (int)Mathf.Pow(2, countIn);
        countOutText.text = "" + countOut;
    }

    void Start2()
    {
        input = new List<bool>();
        output = new List<bool>();

        for(int i = 0; i < countIn; i++)
        {
            input.Add(false);
        }

        for (int i = 0; i < countOut; i++)
        {
            output.Add(false);
        }

        for (int i = 0; i < inputsGO.Count; i++)
        {
            Destroy(inputsGO[i]);
        }
        inputsGO.Clear();
        for (int i = 0; i < outputsGO.Count; i++)
        {
            Destroy(outputsGO[i]);
        }
        outputsGO.Clear();

        float dist1 = (420.0f) / countIn;
        float dist2 = (420.0f) / countOut;

        float x1 = -100.0f;
        float x2 = 100.0f;
        float y1 = 200.0f - dist1 / 2;
        for (int i = 0; i < countIn; i++)
        {
            GameObject createdElement = Instantiate(IGO, image.transform);
            Vector3 newElementPosition = new Vector3
            {
                x = x1,
                y = y1,
                z = 0.0f
            };
            y1 -= dist1;
            createdElement.transform.localPosition = newElementPosition;
            this.inputsGO.Add(createdElement);
        }
        y1 = 200.0f - dist2 / 2;
        for (int i = 0; i < countOut; i++)
        {
            GameObject createdElement = Instantiate(OGO, image.transform);
            Vector3 newElementPosition = new Vector3
            {
                x = x2,
                y = y1,
                z = 0.0f
            };
            y1 -= dist2;
            createdElement.transform.localPosition = newElementPosition;
            this.outputsGO.Add(createdElement);
            //checkb.Add(createdElement.transform.GetComponentInChildren<Toggle>());
            UpdateInput();
            //input.Add(createdElement.transform.GetComponentInChildren<Toggle>().isOn);
        }
    }

    public void UpdateInput()
    {
        for(int i = 0; i < inputsGO.Count; i++)
        {
            input[i] = inputsGO[i].transform.GetComponentInChildren<Toggle>().isOn;

            if (input[i] == false)
            {
                inputsGO[i].transform.GetComponentInChildren<Image>().color = new Color(0.717f, 0.101f, 0.36f);
            }
            else
            {
                inputsGO[i].transform.GetComponentInChildren<Image>().color = new Color(0.101f, 0.717f, 0.36f);
            }
        }
        UpdateOutput();
    }

    void UpdateOutput()
    {
        int coords10 = CreateCoords();
        Debug.Log(coords10);
        for (int i = 0; i < outputsGO.Count; i++)
        {
            if (i == coords10)
            {
                {
                    output[i] = true;
                    outputsGO[i].transform.GetComponentInChildren<Image>().color = new Color(0.101f, 0.717f, 0.36f);//
                }
            }
            else
            {
                output[i] = false;
                outputsGO[i].transform.GetComponentInChildren<Image>().color = new Color(0.717f, 0.101f, 0.36f);
            }
        }
    }

    

    int CreateCoords()
    {
        coords = "";
        for(int i = 0; i < input.Count; i++)
        {
            if (input[i] == false)
                coords += '0';
            else
                coords += '1';
        }
        return Convert.ToInt32(coords, 2);
    }

    public void OnReadInput1()
    {
        int.TryParse(count.text, out int result);
        countIn = result;
        comb.characterLimit = countIn;
        Start1();
        Start2();
    }
}
