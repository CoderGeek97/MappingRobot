using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class DistanceComReader : MonoBehaviour {
    public ObstacalWorld TheObsticalWorld;
    SerialPort sp;
  public  string comPort = "COM7";
	// Use this for initialization
	void Start () {
        TheObsticalWorld = new ObstacalWorld();
        sp = new SerialPort();
        sp.DataReceived += serialPort1_DataReceived;
        sp.PortName = comPort;
        sp.BaudRate = 9600;
        sp.DtrEnable = true;
        sp.Open();
        if (sp.IsOpen)
        {
            print("Open");
        }
	}
    private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
        print("Start Read"); 
        string line = sp.ReadLine();
        print(line);
      //  this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
    }

	// Update is called once per frame
	void Update () {
        string line = sp.ReadLine();
        print(line);
        TheObsticalWorld.AddLine(line);
	}
    public class ObstacalWorld
    {
        public Dictionary<int, Obstical> AllObstacles= new Dictionary<int,Obstical>();
        public void AddLine(string data)
        {
            if (data.Length > 0)
            {
                string[] split = data.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
                int angle = int.Parse(split[0]);
                int dist0 = int.Parse(split[1]);
                int dist1 = int.Parse(split[2]);
                int dist2 = int.Parse(split[3]);

                if (AllObstacles.ContainsKey(angle))
                {
                    Obstical obs = AllObstacles[angle];
                    obs.Distance = dist0;
                }
                else
                {
                    Obstical obs = new Obstical();
                    obs.Angle = angle;
                    obs.Distance = dist0;
                    AllObstacles.Add(angle, obs);
            
                }
            }

        }
    }
    public class Obstical
    {
        public int Angle;
        public int Distance;
        public List<int> LastDistances = new List<int>();

    }
}
