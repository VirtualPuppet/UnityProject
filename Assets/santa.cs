using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.IO;
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

public class santa : MonoBehaviour
{
    private GameObject leftUpperArm, rightUpperArm, leftForeArm, rightForeArm, Eyebrows, Jaw, Smile, LidL, LidR;
    public float left1, left2, left3, left4, left5, left6, right1, right2, right3, right4, right5, right6, M1, M2, M3, angggle , move, action,emotion;
    float direction; // 이동 방향
    float actioncontrol; // 실행중인 행동 종류 
    float weight; // multiple weight to certain roatation axis
    Vector3 velo = Vector3.zero;

    bool nowmove =false; // 이동 애니메이션 실행중인지 확인 하는 변수
    bool nowaction = false; // 행동 애니메이션 실행중인지 확인 하는 변수 
    public Animator animator;


    int count1 = 0; // 이동 애니메이션 실행을 위한 count
    int count2 = 0; // 행동 애니메이션 실행을 위한 count



    bool socketReady = false;
    TcpClient socket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;
    byte[] receivedBuffer;

    Vector3 pastVecLeftUpper = new Vector3(0, 0, 0);
    Vector3 curVecLeftUpper = new Vector3(0, 0, 0);
    Vector3 angleLeftUpper = new Vector3(0, 0, 0);

    Vector3 pastVecLeftFore = new Vector3(0, 0, 0);
    Vector3 curVecLeftFore = new Vector3(0, 0, 0);
    Vector3 angleLeftFore = new Vector3(0, 0, 0);

    Vector3 pastVecRightUpper = new Vector3(0, 0, 0);
    Vector3 curVecRightUpper = new Vector3(0, 0, 0);
    Vector3 angleRightUpper = new Vector3(0, 0, 0);

    Vector3 pastVecRightFore = new Vector3(0, 0, 0);
    Vector3 curVecRightFore = new Vector3(0, 0, 0);
    Vector3 angleRightFore = new Vector3(0, 0, 0);

    Vector3 currentEulerAngle = new Vector3(0, 0, 0);

    bool isFirst = true;
    bool reset = true;
    //int threshold = 1;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

        leftUpperArm = GameObject.Find("TKids L UpperArm");
        rightUpperArm = GameObject.Find("TKids R UpperArm");
        leftForeArm = GameObject.Find("TKids L Forearm");
        rightForeArm = GameObject.Find("TKids R Forearm");
        
        Eyebrows = GameObject.Find("TKids Eyebrows");
        Jaw = GameObject.Find("TKids Jaw");
        Smile = GameObject.Find("TKids Smile");
        LidL = GameObject.Find("TKids LidL");
        LidR = GameObject.Find("TKids LidR");




        //Debug.Log(leftArm.name);
        //transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        ConnectToServer();
        //animator.SetBool("jump", true);
    }

    public void ConnectToServer()
    {
        // 이미 연결되었다면 함수 무시
        if (socketReady) return;

        // 기본 호스트/ 포트번호
        string ip = "192.168.0.3"; //"192.168.0.40", 192.168.0.3
        int port = 8889;

        // 소켓 생성


        socket = new TcpClient(ip, port);
        stream = socket.GetStream();
        writer = new StreamWriter(stream);
        reader = new StreamReader(stream);
        socketReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(30, 0, 0));
        if (socketReady && stream.DataAvailable)
        {
            receivedBuffer = new byte[100];
            stream.Read(receivedBuffer, 0, receivedBuffer.Length); // stream에 있던 바이트배열 내려서 새로 선언한 바이트배열에 넣기

            float msg = BitConverter.ToSingle(receivedBuffer, 0);  // float는 4바이트...
            float msg2 = BitConverter.ToSingle(receivedBuffer, 4);
            float msg3 = BitConverter.ToSingle(receivedBuffer, 8);
            float msg4 = BitConverter.ToSingle(receivedBuffer, 12);  // float는 4바이트...
            float msg5 = BitConverter.ToSingle(receivedBuffer, 16);
            float msg6 = BitConverter.ToSingle(receivedBuffer, 20);

            float msg7 = BitConverter.ToSingle(receivedBuffer, 24);  // float는 4바이트...
            float msg8 = BitConverter.ToSingle(receivedBuffer, 28);
            float msg9 = BitConverter.ToSingle(receivedBuffer, 32);
            float msg10 = BitConverter.ToSingle(receivedBuffer, 36);  // float는 4바이트...
            float msg11 = BitConverter.ToSingle(receivedBuffer, 40);
            float msg12 = BitConverter.ToSingle(receivedBuffer, 44);

            float msg13 = BitConverter.ToSingle(receivedBuffer, 48);  // float는 4바이트...
            float msg14 = BitConverter.ToSingle(receivedBuffer, 52);
            float msg15 = BitConverter.ToSingle(receivedBuffer, 56);

            float msg16 = BitConverter.ToSingle(receivedBuffer, 60);  // action
                                                                        //float msg17 = BitConverter.ToSingle(receivedBuffer, 64); // move
            float msg18 = BitConverter.ToSingle(receivedBuffer, 64); // emotion

            // Debug.Log("x: "+ msg + " y: " + msg2 + " z: " + msg3); // left
            //Debug.Log("x: "+ msg4 + " y: " + msg5 + " z: " + msg6); // right

            //Debug.Log(msg4);
            //Debug.Log(msg5);
            //Debug.Log(msg6);
            //Debug.Log(msg7);
            //Debug.Log(msg8);
            //Debug.Log(msg9);

            left1 = msg;
            left2 = msg2;
            left3 = msg3;
            left4 = msg4;
            left5 = msg5;
            left6 = msg6;
            right1 = msg7;
            right2 = msg8;
            right3 = msg9;
            right4 = msg10;
            right5 = msg11;
            right6 = msg12;
            M1 = msg13;
            M2 = msg14;
            M3 = msg15;
            action = msg16;
            //move = msg17;
            emotion = msg18;

            if (isFirst)
            {
                pastVecLeftUpper.x = left1;
                pastVecLeftUpper.y = left2;
                pastVecLeftUpper.z = left3;
                pastVecLeftFore.x = left4;
                pastVecLeftFore.y = left5;
                pastVecLeftFore.z = left6;

                pastVecRightUpper.x = right1;
                pastVecRightUpper.y = right2;
                pastVecRightUpper.z = right3;
                pastVecRightFore.x = right4;
                pastVecRightFore.y = right5;
                pastVecRightFore.z = right6;
                isFirst = false;
            }
            else
            {

                if (!nowaction && (action == -1f || action == 0f) && !nowmove)
                {
                    // turn off animation
                    if (reset)
                    {
                        leftUpperArm.transform.localRotation = Quaternion.Euler(2, -76, 121);
                        rightUpperArm.transform.localRotation = Quaternion.Euler(7, 71, 95);
                        animator.enabled = false;
                        reset = false;
                    }


                    curVecLeftUpper.x = left1;
                    curVecLeftUpper.y = left2;
                    curVecLeftUpper.z = left3;
                    curVecLeftFore.x = left4;
                    curVecLeftFore.y = left5;
                    curVecLeftFore.z = left6;

                    curVecRightUpper.x = right1;
                    curVecRightUpper.y = right2;
                    curVecRightUpper.z = right3;
                    curVecRightFore.x = right4;
                    curVecRightFore.y = right5;
                    curVecRightFore.z = right6;

                    Debug.Log("past: " + pastVecRightUpper.x + " y: " + pastVecRightUpper.y + " z: " + pastVecRightUpper.z);
                    //Debug.Log("current: " + curVecRightUpper.x + " y: " + curVecRightUpper.y + " z: " + curVecRightUpper.z);

                    angleLeftUpper = getEulerAngleL(1.5f, pastVecLeftUpper, curVecLeftUpper);
                    angleLeftFore = getEulerAngleL(1, pastVecLeftFore, curVecLeftFore);

                    angleRightUpper = getEulerAngleR(1.5f, pastVecRightUpper, curVecRightUpper);
                    angleRightFore = getEulerAngleR(1, pastVecRightFore, curVecRightFore);


                    angggle = rightForeArm.transform.localEulerAngles.y;

                    leftUpperArm.transform.localRotation *= Quaternion.Euler(angleLeftUpper);
                    rightUpperArm.transform.localRotation *= Quaternion.Euler(angleRightUpper);

                    // constraint left fore arm y-axis rotation.
                    if ((leftForeArm.transform.localEulerAngles.y > 300 && leftForeArm.transform.localEulerAngles.y < 360) || (leftForeArm.transform.localEulerAngles.y > 0 && leftForeArm.transform.localEulerAngles.y < 10))
                    {
                        leftForeArm.transform.localRotation *= Quaternion.Euler(angleLeftFore);
                    }
                    else if (leftForeArm.transform.localEulerAngles.y > 10 && leftForeArm.transform.localEulerAngles.y < 50)
                    {
                        leftForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, -10, 0));
                    }
                    else if (leftForeArm.transform.localEulerAngles.y > 260 && leftForeArm.transform.localEulerAngles.y < 300)
                    {
                        leftForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, 10, 0));
                    }

                    //constraint right fore arm y - axis rotation.
                    rightForeArm.transform.localRotation *= Quaternion.Euler(angleRightFore);
                    //if (rightForeArm.transform.localEulerAngles.y > -10 && rightForeArm.transform.localEulerAngles.y < 70)
                    //{
                    //    rightForeArm.transform.localRotation *= Quaternion.Euler(angleRightFore);
                    //}
                    //else if (rightForeArm.transform.localEulerAngles.y > 70 && rightForeArm.transform.localEulerAngles.y < 110)
                    //{
                    //    rightForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, -10, 0));
                    //}
                    //else if (rightForeArm.transform.localEulerAngles.y > 320 && rightForeArm.transform.localEulerAngles.y < 360)
                    //{
                    //    rightForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, 10, 0));
                    //}


                    pastVecLeftUpper = curVecLeftUpper;
                    pastVecRightUpper = curVecRightUpper;

                    pastVecLeftFore = curVecLeftFore;
                    pastVecRightFore = curVecRightFore;

                    //if (emotion == 0f)
                    //{
                    //    animator.SetBool("angry", true);
                    //}
                    //else if (emotion == 1f)
                    //{
                    //    animator.SetBool("happy", true);
                    //}
                    //else if (emotion == 2f)
                    //{
                    //    animator.SetBool("sad", true);
                    //}
                    //else if (emotion == 3f)
                    //{
                    //    animator.SetBool("sad", false);
                    //    animator.SetBool("angry", false);
                    //    animator.SetBool("happy", false);
                    //}

                }
                // 애니메이션 실행 부분
                else if (!nowaction && (action != -1f || action != 0f) && !nowmove)
                {
                    // turn on animation
                    animator.enabled = true;

                    //if (action == 2f)
                    //{ animator.SetBool("walk", true);
                    //    Debug.Log("walk");
                    //    if (move == 1f)
                    //    {
                    //        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(90, Vector3.up));
                    //        Vector3 TT = new Vector3(10f, 0f, 0f);
                    //        transform.position = Vector3.Lerp(transform.position, transform.position + TT, 0.1f); // Player 오브젝트 이동 함수 Time.deltaTime
                    //        Debug.Log("walk11111");
                    //    }
                    //    else if (move == 2f)
                    //    {
                    //        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(-90, Vector3.up));
                    //        Vector3 TT = new Vector3(-10f, 0f, 0f);
                    //        transform.position = Vector3.Lerp(transform.position, transform.position + TT, 0.1f); // Player 오브젝트 이동 함수 Time.deltaTime
                    //        Debug.Log("walk22222");
                    //    }
                    //    else if (move == 3f)
                    //    {
                    //        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(180, Vector3.up));
                    //        Vector3 TT = new Vector3(0f, 0f, 10f);
                    //        transform.position = Vector3.Lerp(transform.position, transform.position + TT, 0.1f); // Player 오브젝트 이동 함수 Time.deltaTime
                    //        Debug.Log("walk33333");
                    //    }
                    //    else if (move == 4f)
                    //    {
                    //        transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(0, Vector3.up));
                    //        Vector3 TT = new Vector3(0f, 0f, -10f);
                    //        transform.position = Vector3.Lerp(transform.position, transform.position + TT, 0.1f); // Player 오브젝트 이동 함수 Time.deltaTime
                    //        Debug.Log("walk44444");
                    //    }
                    //    count1 = count1 + 1;
                    //    nowmove = true;                   

                    //}
                    if (action == 6f || action == 7f || action == 8f || action == 9f)
                    {
                        animator.SetBool("walk", true);
                        Debug.Log("walk");
                        if (action == 9f)
                        {
                            transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(90, Vector3.up));
                            Vector3 TT = new Vector3(10f, 0f, 0f);
                            //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f*Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f*Time.deltaTime);
                            direction = 9f;
                            Debug.Log("walk11111");
                        }
                        else if (action == 8f)
                        {
                            transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(-90, Vector3.up));
                            Vector3 TT = new Vector3(-10f, 0f, 0f);
                            //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f * Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f*Time.deltaTime);
                            direction = 8f;
                            Debug.Log("walk22222");
                        }
                        else if (action == 7f)
                        {
                            transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(180, Vector3.up));
                            Vector3 TT = new Vector3(0f, 0f, -10f);
                            //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f * Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f*Time.deltaTime);
                            direction = 7f;
                            Debug.Log("walk33333");
                        }
                        else if (action == 6f)
                        {
                            transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(0, Vector3.up));
                            Vector3 TT = new Vector3(0f, 0f, 10f);
                            //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f * Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                            transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f*Time.deltaTime);
                            direction = 6f;
                            Debug.Log("walk44444");
                        }
                        count1 = count1 + 1;
                        nowmove = true;

                    }
                    else if (action == 2f)
                    {
                        actioncontrol = action;
                        Debug.Log("action2");
                        animator.SetBool("jump", true);
                        nowaction = true;
                        count2 = count2 + 1;
                    }
                    else if (action == 3f)
                    {
                        actioncontrol = action;
                        Debug.Log("action3");
                        animator.SetBool("run", true);
                        nowaction = true;
                        count2 = count2 + 1;
                    }
                    else if (action == 4f)
                    {
                        actioncontrol = action;
                        Debug.Log("action4");
                        animator.SetBool("fall", true);
                        nowaction = true;
                        count2 = count2 + 1;
                    }
                    else if (action == 5f)
                    {
                        actioncontrol = action;
                        Debug.Log("action5");
                        animator.SetBool("land", true);
                        nowaction = true;
                        count2 = count2 + 1;
                    }

                    else if (action == 1f)
                    {
                        transform.SetPositionAndRotation(new Vector3(-178, 33, -223), Quaternion.Euler(0, 360, 0));
                        isFirst = true;
                        //transform.position = new Vector3(-178, 33, -223);
                        Debug.Log("reset");
                    }

                    Debug.Log("count2: " + count2);
                    reset = true;


                }
                // 애니메이션 실행중일때
                else if (nowaction)
                {
                    count2 = count2 + 1;
                    count2 = count2 % 12;
                    if (animator.enabled)
                    {
                        Debug.Log("count2 loop: " + count2);
                    }
                    if (count2 == 0)
                    {
                        if (actioncontrol == 1f)
                        {

                            animator.SetBool("jump", false);
                            actioncontrol = -1f;
                        }
                        else if (actioncontrol == 3f)
                        {
                            animator.SetBool("run", false);
                            actioncontrol = -1f;
                        }
                        else if (actioncontrol == 4f)
                        {
                            animator.SetBool("fall", false);
                            actioncontrol = -1f;
                        }
                        else if (actioncontrol == 5f)
                        {
                            animator.SetBool("land", false);
                            actioncontrol = -1f;
                        }
                        else if (actioncontrol == 0f)
                        {
                            animator.SetBool("freefall", false);
                            actioncontrol = -1f;
                        }
                        nowaction = false;

                    }



                }
                // 이동중일때
                else if (nowmove) //(heechan) nowmove
                {
                    count1 = count1 + 1;
                    count1 = count1 % 20;
                    Debug.Log("count1 loop: " + count1);
                    // test

                    if (direction == 9f)
                    {
                        Vector3 TT = new Vector3(10f, 0f, 0f);
                        //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f*Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f * Time.deltaTime);
                    }
                    else if (direction == 8f)
                    {
                        Vector3 TT = new Vector3(-10f, 0f, 0f);
                        //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f * Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f * Time.deltaTime);
                    }
                    else if (direction == 7f)
                    {
                        Vector3 TT = new Vector3(0f, 0f, -10f);
                        //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f * Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f * Time.deltaTime);
                    }
                    else if (direction == 6f)
                    {
                        Vector3 TT = new Vector3(0f, 0f, 10f);
                        //transform.position = Vector3.Lerp(transform.position, transform.position + TT, 1f * Time.deltaTime); // Player 오브젝트 이동 함수 Time.deltaTime
                        transform.position = Vector3.MoveTowards(transform.position, transform.position + TT, 10f * Time.deltaTime);
                    }

                    if (count1 == 0)
                    {

                        animator.SetBool("walk", false);
                        actioncontrol = -1f;
                        nowmove = false;
                    }
                }



                if (emotion == 0f)
                {
                    Vector3 Eyebrows0 = new Vector3(-0.2f, 0.02f, 5.2211e-08f);
                    Vector3 Jaw0 = new Vector3(-0.12f, 0.035f, 0f);
                    Vector3 Smile0 = new Vector3(-0.05f, 0f, 9.7789e-11f);
                    Vector3 LidL0 = new Vector3(-0.18f, 0.097969f, 0.064696f);
                    Vector3 LidR0 = new Vector3(-0.18f, 0.097969f, -0.064696f);
                    Eyebrows.transform.localPosition = Eyebrows0;
                    Jaw.transform.localPosition = Jaw0;
                    Smile.transform.localPosition = Smile0;
                    LidL.transform.localPosition = LidL0;
                    LidR.transform.localPosition = LidR0;
                    //animator.SetBool("angry", true);
                }
                else if (emotion == 1f)
                {
                    Vector3 Eyebrows1 = new Vector3(-0.27f, 0.020841f, 5.2211e-08f);
                    Vector3 Jaw1 = new Vector3(-0.122f, 0.04f, 0f);
                    Vector3 Smile1 = new Vector3(-0.02f, -0.02f, 9.7789e-11f);
                    Vector3 LidL1 = new Vector3(-0.129f, 0.12f, 0.059f);
                    Vector3 LidR1 = new Vector3(-0.129f, 0.12f, -0.059f);
                    Eyebrows.transform.localPosition = Eyebrows1;
                    Jaw.transform.localPosition = Jaw1;
                    Smile.transform.localPosition = Smile1;
                    LidL.transform.localPosition = LidL1;
                    LidR.transform.localPosition = LidR1;
                    //animator.SetBool("happy", true);
                }
                else if (emotion == 2f)
                {
                    Vector3 Eyebrows2 = new Vector3(-0.209f, 0.023f, 5.2211e-08f);
                    Vector3 Jaw2 = new Vector3(-0.126f, 0.047f, 0f);
                    Vector3 Smile2 = new Vector3(-0.05f, 0.04f, 9.7789e-11f);
                    Vector3 LidL2 = new Vector3(-0.149f, 0.097969f, 0.0605f);
                    Vector3 LidR2 = new Vector3(-0.149f, 0.097969f, -0.0605f);
                    Eyebrows.transform.localPosition = Eyebrows2;
                    Jaw.transform.localPosition = Jaw2;
                    Smile.transform.localPosition = Smile2;
                    LidL.transform.localPosition = LidL2;
                    LidR.transform.localPosition = LidR2;
                    //animator.SetBool("sad", true);
                }
                else if (emotion == 3f)
                {
                    Vector3 Eyebrows3 = new Vector3(-0.27f, 0.020841f, 5.2211e-08f);
                    Vector3 Jaw3 = new Vector3(-0.122f, 0.04f, 0f);
                    Vector3 Smile3 = new Vector3(-0.05f, -0f, 9.7789e-11f);
                    Vector3 LidL3 = new Vector3(-0.16f, 0.12f, 0.059f);
                    Vector3 LidR3 = new Vector3(-0.16f, 0.12f, -0.059f);
                    Eyebrows.transform.localPosition = Eyebrows3;
                    Jaw.transform.localPosition = Jaw3;
                    Smile.transform.localPosition = Smile3;
                    LidL.transform.localPosition = LidL3;
                    LidR.transform.localPosition = LidR3;
                    //animator.SetBool("happy", true);
                }




            }
        }
    }



    // rotation for left hand - thumb
    Vector3 getEulerAngleL(float weight, Vector3 past, Vector3 cur)
    {
        float magnX, magnY, magnZ;
        float angleX, angleY, angleZ;
        Vector3 crossX = Vector3.Cross(new Vector3(0f, past.y, past.z), new Vector3(0f, cur.y, cur.z));
        Vector3 crossY = Vector3.Cross(new Vector3(past.x, 0f, past.z), new Vector3(cur.x, 0f, cur.z));
        Vector3 crossZ = Vector3.Cross(new Vector3(past.x, past.y, 0f), new Vector3(cur.x, cur.y, 0f));

        if (crossX.x > 0)
        {
            magnX = -Vector3.Magnitude(crossX);
        }
        else
        {
            magnX = Vector3.Magnitude(crossX);
        }

        if (crossY.y > 0)
        {
            magnY = Vector3.Magnitude(crossY);
        }
        else
        {
            magnY = -Vector3.Magnitude(crossY);
        }

        if (weight == 1.5) // Upeer Arm
        {
            if (crossZ.z > 0)
            {
                magnZ = -Vector3.Magnitude(crossZ);
            }
            else
            {
                magnZ = Vector3.Magnitude(crossZ);
            }
        }
        else // Fore Arm
        {
            if (crossZ.z > 0)
            {
                magnZ = Vector3.Magnitude(crossZ);
            }
            else
            {
                magnZ = -Vector3.Magnitude(crossZ);
            }
        }
        Debug.Log("Magn: " + magnX + " " + magnY + " " + magnZ);

        // get angle using cross product
        angleX = Mathf.Rad2Deg * Mathf.Asin(magnX / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
        angleY = Mathf.Rad2Deg * Mathf.Asin(magnY / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
        angleZ = Mathf.Rad2Deg * Mathf.Asin(magnZ / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));
        if(float.IsNaN(angleX) || float.IsNaN(angleY) || float.IsNaN(angleZ))
        {
            angleX = 0;
            angleY = 0;
            angleZ = 0;
        }
        // get angle using dot product
        float DangleX = Mathf.Rad2Deg * Mathf.Acos(((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
        float DangleY = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.z * cur.z)) / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
        float DangleZ = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.y * cur.y)) / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));

        //Debug.Log("Past: " + past);
        //Debug.Log("Cur: " + cur);
        //Debug.Log((past.y * cur.y) + (past.z * cur.z) + " " + Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) + " " + Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z)) + " " + ((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
        //Debug.Log("angleX: " + angleX);
        //Debug.Log("angleY: " + angleY);
        //Debug.Log("angleZ: " + angleZ);
        Debug.Log("angle: " + angleX + " " + angleY + " " + angleZ);
        //Debug.Log(DangleX + " " + DangleY + " " + DangleZ);
        //Debug.Log("-------------------------------------------");

        if(weight == 1.5f) return new Vector3(0, angleZ, weight * angleX); // Upper Arm
        else return new Vector3(0, angleY, 0); // Fore Arm
        //return new Vector3(0.1f, angleY, angleZ);
    }



    //Quaternion getQuat(Vector3 past, Vector3 cur)
    //{
    //    Vector3 crossVec = Vector3.Cross(new Vector3(past.x, past.y, past.z), new Vector3(cur.x, cur.y, cur.z));
    //    float magn = Vector3.Magnitude(crossVec);
    //    Vector3 unitVec = crossVec / magn;
    //    float angle = Vector3.Angle(past, cur) / 2;
    //    float w = Mathf.Cos(angle);
    //    float x = Mathf.Sin(angle) * unitVec.x;
    //    float y = Mathf.Sin(angle) * unitVec.y;
    //    float z = Mathf.Sin(angle) * unitVec.z;
    //    return new Quaternion(w, x, y, z);
    //}



    // rotation for right hand - middle finger
    Vector3 getEulerAngleR(float weight, Vector3 past, Vector3 cur)
    {
        float magnX, magnY, magnZ;
        float angleX, angleY, angleZ;
        Vector3 crossX = Vector3.Cross(new Vector3(0f, past.y, past.z), new Vector3(0f, cur.y, cur.z));
        Vector3 crossY = Vector3.Cross(new Vector3(past.x, 0f, past.z), new Vector3(cur.x, 0f, cur.z));
        Vector3 crossZ = Vector3.Cross(new Vector3(past.x, past.y, 0f), new Vector3(cur.x, cur.y, 0f));

        if (weight == 1.5) // Upper Arm
        {
            if (crossX.x > 0)
            {
                magnX = -Vector3.Magnitude(crossX);
            }
            else
            {
                magnX = Vector3.Magnitude(crossX);
            }
        }
        else // Fore Arm
        {
            if (crossX.x > 0)
            {
                magnX = Vector3.Magnitude(crossX);
            }
            else
            {
                magnX = -Vector3.Magnitude(crossX);
            }
        }

        if (crossY.y > 0)
        {
            magnY = -Vector3.Magnitude(crossY);
        }
        else
        {
            magnY = Vector3.Magnitude(crossY);
        }

        if (crossZ.z > 0)
        {
            magnZ = -Vector3.Magnitude(crossZ);
        }
        else
        {
            magnZ = Vector3.Magnitude(crossZ);
        }

        if (crossZ.z > 0)
        {
            magnZ = -Vector3.Magnitude(crossZ);
        }
        else
        {
            magnZ = Vector3.Magnitude(crossZ);
        }
        Debug.Log("Magn: " + magnX + " " + magnY + " " + magnZ);

        // get angle using cross product
        angleX = Mathf.Rad2Deg * Mathf.Asin(magnX / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
        angleY = Mathf.Rad2Deg * Mathf.Asin(magnY / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
        angleZ = Mathf.Rad2Deg * Mathf.Asin(magnZ / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));
        if (float.IsNaN(angleX) || float.IsNaN(angleY) || float.IsNaN(angleZ))
        {
            angleX = 0;
            angleY = 0;
            angleZ = 0;
        }
        // get angle using dot product
        float DangleX = Mathf.Rad2Deg * Mathf.Acos(((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
        float DangleY = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.z * cur.z)) / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
        float DangleZ = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.y * cur.y)) / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));

        //Debug.Log("Past: " + past);
        //Debug.Log("Cur: " + cur);
        //Debug.Log((past.y * cur.y) + (past.z * cur.z) + " " + Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) + " " + Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z)) + " " + ((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
        //Debug.Log("angleX: " + angleX);
        //Debug.Log("angleY: " + angleY);
        //Debug.Log("angleZ: " + angleZ);
        Debug.Log("angle: " + angleX + " " + angleY + " " + angleZ);
        //Debug.Log(DangleX + " " + DangleY + " " + DangleZ);
        //Debug.Log("-------------------------------------------");

        if (weight == 1.5f) return new Vector3(0, angleZ, weight * angleX); // Upper Arm
        else return new Vector3(0, angleX, 0); // Fore Arm
    }



    void OnApplicationQuit()
    {
        CloseSocket();
    }

    void CloseSocket()
    {
        if (!socketReady) return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }
}
