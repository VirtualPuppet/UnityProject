//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Net.Sockets;
//using System.IO;
//using System;
//using System.Text;
//using System.Runtime.InteropServices;
//using System.Runtime.InteropServices.ComTypes;

//public class test : MonoBehaviour
//{
//    private GameObject leftUpperArm, rightUpperArm, leftForeArm, rightForeArm;
//    public float left1, left2, left3, left4, left5, left6, right1, right2, right3, right4, right5, right6, M1, M2, M3, angggle;
//    float weight; // multiple weight to certain roatation axis

//    bool socketReady = false;
//    TcpClient socket;
//    NetworkStream stream;
//    StreamWriter writer;
//    StreamReader reader;
//    byte[] receivedBuffer;

//    Vector3 pastVecLeftUpper = new Vector3(0, 0, 0);
//    Vector3 curVecLeftUpper = new Vector3(0, 0, 0);
//    Vector3 angleLeftUpper = new Vector3(0, 0, 0);

//    Vector3 pastVecLeftFore = new Vector3(0, 0, 0);
//    Vector3 curVecLeftFore = new Vector3(0, 0, 0);
//    Vector3 angleLeftFore = new Vector3(0, 0, 0);

//    Vector3 pastVecRightUpper = new Vector3(0, 0, 0);
//    Vector3 curVecRightUpper = new Vector3(0, 0, 0);
//    Vector3 angleRightUpper = new Vector3(0, 0, 0);

//    Vector3 pastVecRightFore = new Vector3(0, 0, 0);
//    Vector3 curVecRightFore = new Vector3(0, 0, 0);
//    Vector3 angleRightFore = new Vector3(0, 0, 0);

//    Vector3 currentEulerAngle = new Vector3(0, 0, 0);

//    bool isFirst = true;
//    //int threshold = 1;

//    // Start is called before the first frame update
//    void Start()
//    {
//        leftUpperArm = GameObject.Find("B-upper_arm_L");
//        rightUpperArm = GameObject.Find("B-upper_arm_R");
//        leftForeArm = GameObject.Find("B-forearm_L");
//        rightForeArm = GameObject.Find("B-forearm_R");
//        //Debug.Log(leftArm.name);
//        //transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
//        ConnectToServer();
//    }

//    public void ConnectToServer()
//    {
//        // 이미 연결되었다면 함수 무시
//        if (socketReady) return;

//        // 기본 호스트/ 포트번호
//        string ip = "192.168.0.40";
//        int port = 8889;

//        // 소켓 생성


//        socket = new TcpClient(ip, port);
//        stream = socket.GetStream();
//        writer = new StreamWriter(stream);
//        reader = new StreamReader(stream);
//        socketReady = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //transform.rotation = Quaternion.Euler(new Vector3(30, 0, 0));
//        if (socketReady && stream.DataAvailable)
//        {
//            receivedBuffer = new byte[100];
//            stream.Read(receivedBuffer, 0, receivedBuffer.Length); // stream에 있던 바이트배열 내려서 새로 선언한 바이트배열에 넣기
//            float msg = BitConverter.ToSingle(receivedBuffer, 0);  // float는 4바이트...
//            float msg2 = BitConverter.ToSingle(receivedBuffer, 4);
//            float msg3 = BitConverter.ToSingle(receivedBuffer, 8);
//            float msg4 = BitConverter.ToSingle(receivedBuffer, 12);  // float는 4바이트...
//            float msg5 = BitConverter.ToSingle(receivedBuffer, 16);
//            float msg6 = BitConverter.ToSingle(receivedBuffer, 20);
//            float msg7 = BitConverter.ToSingle(receivedBuffer, 24);  // float는 4바이트...
//            float msg8 = BitConverter.ToSingle(receivedBuffer, 28);
//            float msg9 = BitConverter.ToSingle(receivedBuffer, 32);
//            float msg10 = BitConverter.ToSingle(receivedBuffer, 36);  // float는 4바이트...
//            float msg11 = BitConverter.ToSingle(receivedBuffer, 40);
//            float msg12 = BitConverter.ToSingle(receivedBuffer, 44);
//            float msg13 = BitConverter.ToSingle(receivedBuffer, 48);  // float는 4바이트...
//            float msg14 = BitConverter.ToSingle(receivedBuffer, 52);
//            float msg15 = BitConverter.ToSingle(receivedBuffer, 56);

//            // Debug.Log("x: "+ msg + " y: " + msg2 + " z: " + msg3); // left
//            //Debug.Log("x: "+ msg4 + " y: " + msg5 + " z: " + msg6); // right

//            //Debug.Log(msg4);
//            //Debug.Log(msg5);
//            //Debug.Log(msg6);
//            //Debug.Log(msg7);
//            //Debug.Log(msg8);
//            //Debug.Log(msg9);
//            left1 = msg;
//            left2 = msg2;
//            left3 = msg3;
//            left4 = msg4;
//            left5 = msg5;
//            left6 = msg6;
//            right1 = msg7;
//            right2 = msg8;
//            right3 = msg9;
//            right4 = msg10;
//            right5 = msg11;
//            right6 = msg12;
//            M1 = msg13;
//            M2 = msg14;
//            M3 = msg15;

//            if (isFirst)
//            {
//                pastVecLeftUpper.x = left1;
//                pastVecLeftUpper.y = left2;
//                pastVecLeftUpper.z = left3;
//                pastVecLeftFore.x = left4;
//                pastVecLeftFore.y = left5;
//                pastVecLeftFore.z = left6;

//                pastVecRightUpper.x = right1;
//                pastVecRightUpper.y = right2;
//                pastVecRightUpper.z = right3;
//                pastVecRightFore.x = right4;
//                pastVecRightFore.y = right5;
//                pastVecRightFore.z = right6;
//                isFirst = false;
//            }
//            else
//            {
//                curVecLeftUpper.x = left1;
//                curVecLeftUpper.y = left2;
//                curVecLeftUpper.z = left3;
//                curVecLeftFore.x = left4;
//                curVecLeftFore.y = left5;
//                curVecLeftFore.z = left6;

//                curVecRightUpper.x = right1;
//                curVecRightUpper.y = right2;
//                curVecRightUpper.z = right3;
//                curVecRightFore.x = right4;
//                curVecRightFore.y = right5;
//                curVecRightFore.z = right6;

//                //Debug.Log("past: " + pastVecRightUpper.x + " y: " + pastVecRightUpper.y + " z: " + pastVecRightUpper.z);
//                //Debug.Log("current: " + curVecRightUpper.x + " y: " + curVecRightUpper.y + " z: " + curVecRightUpper.z);

//                angleLeftUpper = getEulerAngleL(1.5f, pastVecLeftUpper, curVecLeftUpper);
//                angleLeftFore = getEulerAngleL(1, pastVecLeftFore, curVecLeftFore);
//                angleLeftFore.y = 0;

//                angleRightUpper = getEulerAngleR(1.5f, pastVecRightUpper, curVecRightUpper);
//                angleRightFore = getEulerAngleR(1, pastVecRightFore, curVecRightFore);
//                angleRightFore.y = 0;

//                angggle = leftUpperArm.transform.localEulerAngles.y;

//                leftUpperArm.transform.localRotation *= Quaternion.Euler(angleLeftUpper);
//                rightUpperArm.transform.localRotation *= Quaternion.Euler(angleRightUpper);

//                // constraint left fore arm z-axis rotation.
//                if (leftForeArm.transform.localEulerAngles.z > 0 && leftForeArm.transform.localEulerAngles.z < 90)
//                {
//                    leftForeArm.transform.localRotation *= Quaternion.Euler(angleLeftFore);
//                }
//                else if (leftForeArm.transform.localEulerAngles.z > 320 && leftForeArm.transform.localEulerAngles.z < 360)
//                {
//                    leftForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, 10));
//                }
//                else if (leftForeArm.transform.localEulerAngles.z > 90)
//                {
//                    leftForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, -10));
//                }

//                // constraint right fore arm z-axis rotation.
//                if (rightForeArm.transform.localEulerAngles.z > 270 && rightForeArm.transform.localEulerAngles.z < 360)
//                {
//                    rightForeArm.transform.localRotation *= Quaternion.Euler(angleRightFore);
//                }
//                else if (rightForeArm.transform.localEulerAngles.z > 0 && rightForeArm.transform.localEulerAngles.z < 40)
//                {
//                    rightForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, -10));
//                }
//                else if (rightForeArm.transform.localEulerAngles.z < 270)
//                {
//                    rightForeArm.transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, 10));
//                }


//                pastVecLeftUpper = curVecLeftUpper;
//                pastVecRightUpper = curVecRightUpper;

//                pastVecLeftFore = curVecLeftFore;
//                pastVecRightFore = curVecRightFore;

//            }
//        }
//    }

//    // rotation for left hand - thumb
//    Vector3 getEulerAngleL(float weight, Vector3 past, Vector3 cur)
//    {
//        float magnX, magnY, magnZ;
//        Vector3 crossX = Vector3.Cross(new Vector3(0f, past.y, past.z), new Vector3(0f, cur.y, cur.z));
//        Vector3 crossY = Vector3.Cross(new Vector3(past.x, 0f, past.z), new Vector3(cur.x, 0f, cur.z));
//        Vector3 crossZ = Vector3.Cross(new Vector3(past.x, past.y, 0f), new Vector3(cur.x, cur.y, 0f));

//        if (crossX.x > 0)
//        {
//            magnX = -Vector3.Magnitude(crossX);
//        }
//        else
//        {
//            magnX = Vector3.Magnitude(crossX);
//        }

//        if (crossY.y > 0)
//        {
//            magnY = Vector3.Magnitude(crossY);
//        }
//        else
//        {
//            magnY = -Vector3.Magnitude(crossY);
//        }

//        if (crossZ.z > 0)
//        {
//            magnZ = Vector3.Magnitude(crossZ);
//        }
//        else
//        {
//            magnZ = -Vector3.Magnitude(crossZ);
//        }
//        Debug.Log("Magn: " + magnX + " " + magnY + " " + magnZ);

//        // get angle using cross product
//        float angleX = Mathf.Rad2Deg * Mathf.Asin(magnX / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
//        float angleY = Mathf.Rad2Deg * Mathf.Asin(magnY / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
//        float angleZ = Mathf.Rad2Deg * Mathf.Asin(magnZ / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));

//        // get angle using dot product
//        float DangleX = Mathf.Rad2Deg * Mathf.Acos(((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
//        float DangleY = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.z * cur.z)) / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
//        float DangleZ = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.y * cur.y)) / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));

//        //Debug.Log("Past: " + past);
//        //Debug.Log("Cur: " + cur);
//        //Debug.Log((past.y * cur.y) + (past.z * cur.z) + " " + Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) + " " + Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z)) + " " + ((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
//        //Debug.Log("angleX: " + angleX);
//        //Debug.Log("angleY: " + angleY);
//        //Debug.Log("angleZ: " + angleZ);
//        Debug.Log("angle: " + angleX + " " + angleY + " " + angleZ);
//        //Debug.Log(DangleX + " " + DangleY + " " + DangleZ);
//        //Debug.Log("-------------------------------------------");

//        return new Vector3(0, weight * angleX, angleZ);
//        //return new Vector3(0.1f, angleY, angleZ);
//    }

//    //Quaternion getQuat(Vector3 past, Vector3 cur)
//    //{
//    //    Vector3 crossVec = Vector3.Cross(new Vector3(past.x, past.y, past.z), new Vector3(cur.x, cur.y, cur.z));
//    //    float magn = Vector3.Magnitude(crossVec);
//    //    Vector3 unitVec = crossVec / magn;
//    //    float angle = Vector3.Angle(past, cur) / 2;
//    //    float w = Mathf.Cos(angle);
//    //    float x = Mathf.Sin(angle) * unitVec.x;
//    //    float y = Mathf.Sin(angle) * unitVec.y;
//    //    float z = Mathf.Sin(angle) * unitVec.z;
//    //    return new Quaternion(w, x, y, z);
//    //}

//    // rotation for right hand - middle finger
//    Vector3 getEulerAngleR(float weight, Vector3 past, Vector3 cur)
//    {
//        float magnX, magnY, magnZ;
//        Vector3 crossX = Vector3.Cross(new Vector3(0f, past.y, past.z), new Vector3(0f, cur.y, cur.z));
//        Vector3 crossY = Vector3.Cross(new Vector3(past.x, 0f, past.z), new Vector3(cur.x, 0f, cur.z));
//        Vector3 crossZ = Vector3.Cross(new Vector3(past.x, past.y, 0f), new Vector3(cur.x, cur.y, 0f));

//        if (crossX.x > 0)
//        {
//            magnX = -Vector3.Magnitude(crossX);
//        }
//        else
//        {
//            magnX = Vector3.Magnitude(crossX);
//        }

//        if (crossY.y > 0)
//        {
//            magnY = Vector3.Magnitude(crossY);
//        }
//        else
//        {
//            magnY = -Vector3.Magnitude(crossY);
//        }

//        if (crossZ.z > 0)
//        {
//            magnZ = Vector3.Magnitude(crossZ);
//        }
//        else
//        {
//            magnZ = -Vector3.Magnitude(crossZ);
//        }
//        Debug.Log("Magn: " + magnX + " " + magnY + " " + magnZ);

//        // get angle using cross product
//        float angleX = Mathf.Rad2Deg * Mathf.Asin(magnX / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
//        float angleY = Mathf.Rad2Deg * Mathf.Asin(magnY / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
//        float angleZ = Mathf.Rad2Deg * Mathf.Asin(magnZ / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));

//        // get angle using dot product
//        float DangleX = Mathf.Rad2Deg * Mathf.Acos(((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
//        float DangleY = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.z * cur.z)) / (Mathf.Sqrt((past.x * past.x) + (past.z * past.z)) * Mathf.Sqrt((cur.x * cur.x) + (cur.z * cur.z))));
//        float DangleZ = Mathf.Rad2Deg * Mathf.Acos(((past.x * cur.x) + (past.y * cur.y)) / (Mathf.Sqrt((past.x * past.x) + (past.y * past.y)) * Mathf.Sqrt((cur.x * cur.x) + (cur.y * cur.y))));

//        //Debug.Log("Past: " + past);
//        //Debug.Log("Cur: " + cur);
//        //Debug.Log((past.y * cur.y) + (past.z * cur.z) + " " + Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) + " " + Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z)) + " " + ((past.y * cur.y) + (past.z * cur.z)) / (Mathf.Sqrt((past.y * past.y) + (past.z * past.z)) * Mathf.Sqrt((cur.y * cur.y) + (cur.z * cur.z))));
//        //Debug.Log("angleX: " + angleX);
//        //Debug.Log("angleY: " + angleY);
//        //Debug.Log("angleZ: " + angleZ);
//        Debug.Log("angle: " + angleX + " " + angleY + " " + angleZ);
//        //Debug.Log(DangleX + " " + DangleY + " " + DangleZ);
//        //Debug.Log("-------------------------------------------");

//        return new Vector3(0, weight * angleX, angleZ);
//    }

//    void OnApplicationQuit()
//    {
//        CloseSocket();
//    }

//    void CloseSocket()
//    {
//        if (!socketReady) return;

//        writer.Close();
//        reader.Close();
//        socket.Close();
//        socketReady = false;
//    }
//}
