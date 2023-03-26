using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using System.IO.Ports;

using IronPython.Hosting;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Text;
using MindFusion.Mapping;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Syncfusion.WinForms.Controls;
using System.Threading;
using SharpYaml.Tokens;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using static IronPython.Modules._ast;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        MAVLink.MavlinkParse mavlink = new MAVLink.MavlinkParse();
        bool armed = false;
        // locking to prevent multiple reads on serial port
        object readlock = new object();
        // our target sysid
        byte sysid;
        // our target compid
        byte compid;
        SerialPort serialPort1 = new SerialPort();

        //Buton tasarım kodu
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundREctRgn
            (
                int nLeft,
                int nTop,
                int nRight,
                int nBottom,
                int nWidthEllipse,
                int nHeightEll
               
            );

        public Form1()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            //;
            /*
             * // Seri port ayarlarını tanımla
            int baudRate = 57600;
            int dataBits = 8;
            Parity parity = Parity.None;
            StopBits stopBits = StopBits.One;
            string portName = "COM8";

            // Seri port nesnesini oluştur
            SerialPort serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            // Seri portu aç
            serialPort.Open();
            // Seri porttan verileri oku
            string data = serialPort.ReadLine();
            label18.Text = data.ToString();
            serialPort.Close();
            var source = @"C:\Users\Turqay\WindowsFormsApp1.2\dinamic.py";
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            var oper = engine.Operations;

            engine.ExecuteFile(source, scope);

            var Hesap = scope.GetVariable("Hesap");
            dynamic ins = oper.CreateInstance(Hesap);  label19.Text = ins.topla(15, 20).ToString();
            */




            //bb
            sfButton1.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0, 
                sfButton1.Width, sfButton1.Height, 10, 10));

            parasutac.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
                parasutac.Width, parasutac.Height, 10, 10));

            ucusagec.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
                ucusagec.Width, ucusagec.Height, 10, 10));

            manuel.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
                manuel.Width, manuel.Height, 10, 10));

            otonom.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
                otonom.Width, otonom.Height, 10, 10));

            takeoff.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
                takeoff.Width, takeoff.Height, 10, 10));

            hedefidegis.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
               hedefidegis.Width, hedefidegis.Height, 20, 20));

            land.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
                land.Width, land.Height, 10, 10));

           

            armDis.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
               armDis.Width, armDis.Height, 20, 20));

            gonder.Region = Region.FromHrgn(CreateRoundREctRgn(0, 0,
                gonder.Width, gonder.Height, 20, 20));



            //  MainWindow mainWindow = new MainWindow();
            // mainWindow.MdiParent = this;//
            // mainWindow.Show(); //Yeni Pencere 

            map.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
          
            //Enlem Boylam
            map.DragButton = MouseButtons.Right;
            map.MapProvider = GMapProviders.GoogleSatelliteMap;
            map.MarkersEnabled = true;
            map.PolygonsEnabled= true;  
            double lat = 40.753602; //ennlem
            double lng = 30.353512; //boylam
            map.Position = new PointLatLng(lat, lng);
            map.MinZoom = 1; //Minumum Zoom level
            map.MaxZoom = 20; //
            map.Zoom = 20; //Current Zoom Level

            Bitmap biz = new Bitmap(WindowsFormsApp1.AvionicsInstrumentsControls.AvionicsInstrumentsControlsRessources.Biz);
            Bitmap hedef = new Bitmap(WindowsFormsApp1.AvionicsInstrumentsControls.AvionicsInstrumentsControlsRessources.Hedef);
            Bitmap hedefolmayan = new Bitmap(WindowsFormsApp1.AvionicsInstrumentsControls.AvionicsInstrumentsControlsRessources.HedefOlmayan);

          
            //Konum

            // hedef konum
           // map.Overlays.Add(otherPlanes(40.753604, 30.353504, hedef));
            map.Overlays.Add(otherPlanes(40.753604, 30.353404, hedef));
            map.Overlays.Add(otherPlanes(40.753404, 30.353304, hedef));
            //hedef olmayan konum
             map.Overlays.Add(otherPlanes(40.753730, 30.353500, hedefolmayan));
           // map.Overlays.Add(otherPlanes(40.753682, 30.353582, hedefolmayan));

            //bizim konum
            map.Overlays.Add(otherPlanes(40.753602, 30.353512, biz));

            // Yarış alanı
            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            IList points = new List<PointLatLng>(); 
            points.Add(new PointLatLng(40.969562, 30.585789));
            points.Add(new PointLatLng(40.966205, 30.588171));
            points.Add(new PointLatLng(40.968134, 30.591647));
            points.Add(new PointLatLng(40.971684, 30.589759));
            GMapPolygon polygon = new GMapPolygon((List<PointLatLng>)points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Add(polygon);
            map.Overlays.Add(polyOverlay);
           

        }
      
        private void sfButton1_Click(object sender, EventArgs e)
        {
            // if the port is open close it
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                return;
            }

            // set the comport options
            serialPort1.PortName = comboBox3.Text;
            serialPort1.BaudRate = int.Parse(comboBox4.Text);

            // open the comport
            serialPort1.Open();

            // set timeout to 2 seconds
            serialPort1.ReadTimeout = 2000;

            BackgroundWorker bgw = new BackgroundWorker();

            bgw.DoWork += bgw_DoWork;
            
            bgw.RunWorkerAsync();
        }
        int deger = 0;
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            while (serialPort1.IsOpen)
            {
                try
                {
                    MAVLink.MAVLinkMessage packet;
                    lock (readlock)
                    {
                        // read any valid packet from the port
                        packet = mavlink.ReadPacket(serialPort1.BaseStream);

                        // check its valid
                        if (packet == null || packet.data == null)
                            continue;
                    }

                    // check to see if its a hb packet from the comport
                    if (packet.data.GetType() == typeof(MAVLink.mavlink_heartbeat_t))
                    {
                        var hb = (MAVLink.mavlink_heartbeat_t)packet.data;

                        // save the sysid and compid of the seen MAV
                        sysid = packet.sysid;
                        compid = packet.compid;

                        // request streams at 2 hz
                        var buffer = mavlink.GenerateMAVLinkPacket10(MAVLink.MAVLINK_MSG_ID.REQUEST_DATA_STREAM,
                            new MAVLink.mavlink_request_data_stream_t()
                            {
                                req_message_rate = 2,
                                req_stream_id = (byte)MAVLink.MAV_DATA_STREAM.ALL,
                                start_stop = 1,
                                target_component = compid,
                                target_system = sysid
                            });

                        serialPort1.Write(buffer, 0, buffer.Length);

                        buffer = mavlink.GenerateMAVLinkPacket10(MAVLink.MAVLINK_MSG_ID.HEARTBEAT, hb);

                        serialPort1.Write(buffer, 0, buffer.Length);
                    }

                    // from here we should check the the message is addressed to us
                    if (sysid != packet.sysid || compid != packet.compid)
                        continue;

                    if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.BATTERY_STATUS)
                    {
                        var att = (MAVLink.mavlink_battery_status_t)packet.data;
                        label19.Text= att.current_battery.ToString();
                    }

                    if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.ATTITUDE)
                    {
                        var att = (MAVLink.mavlink_attitude_t)packet.data;
                        turnCoordinatorInstrumentControl1.SetTurnCoordinatorParameters(att.roll * 10, att.yawspeed * 10);
                        horizonInstrumentControl1.SetAttitudeIndicatorParameters(att.pitch * 57.2958, att.roll * 57.2958);
                    }

                    if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.VFR_HUD)
                    {
                        var vh = (MAVLink.mavlink_vfr_hud_t)packet.data;
                                       
                        yukseklik.Value = Convert.ToInt32(vh.alt);
                        double altD= vh.alt*100;
                        altimeterInstrumentControl1.SetAlimeterParameters(Convert.ToInt32(altD));
                        yerhizigosterge.Value = Convert.ToInt32(vh.groundspeed);
                        airSpeedInstrumentControl1.SetAirSpeedIndicatorParameters(vh.groundspeed * 100); // yer hizi
                        verticalSpeedInstrumentControl1.SetVerticalSpeedIndicatorParameters(vh.airspeed*100); // hava hizi
                        headingIndicatorInstrumentControl1.SetHeadingIndicatorParameters(vh.heading );//heading
                    }

                   

                    if (packet.msgid == (byte)MAVLink.MAVLINK_MSG_ID.ATTITUDE)
                    //or
                    //if (packet.data.GetType() == typeof(MAVLink.mavlink_attitude_t))
                    {
                        var att = (MAVLink.mavlink_attitude_t)packet.data;
                        turnCoordinatorInstrumentControl1.SetTurnCoordinatorParameters(att.roll*10, att.yawspeed*10);
                        horizonInstrumentControl1.SetAttitudeIndicatorParameters(att.pitch*57.2958, att.roll*57.2958);
                    }
                }
                catch
                {
                    
                }
                
                //System.Threading.Thread.Sleep(1);
            }
        }
        T readsomedata<T>(byte sysid, byte compid, int timeout = 2000)
        {
            DateTime deadline = DateTime.Now.AddMilliseconds(timeout);

            lock (readlock)
            {
                // read the current buffered bytes
                while (DateTime.Now < deadline)
                {
                    var packet = mavlink.ReadPacket(serialPort1.BaseStream);

                    // check its not null, and its addressed to us
                    if (packet == null || sysid != packet.sysid || compid != packet.compid)
                        continue;

                   

                    if (packet.data.GetType() == typeof(T))
                    {
                        return (T)packet.data;
                    }
                }
            }

            throw new Exception("No packet match found");
        }

        public GMapOverlay otherPlanes(double lat, double lng, Bitmap bitmap)
        {
            
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lng),
              bitmap);
            markersOverlay.Markers.Add(marker);

            return markersOverlay;
        }
      
        public void DrawLinePointF(PaintEventArgs e)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points that define line.
            PointF point1 = new PointF(100.0F, 100.0F);
            PointF point2 = new PointF(500.0F, 100.0F);

            // Draw line to screen.
            e.Graphics.DrawLine(blackPen, point1, point2);
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            comboBox3.DataSource = SerialPort.GetPortNames();
        }

        private void sfButton2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToLongTimeString();
        }

        private void armDis_Click(object sender, EventArgs e)
        {
            MAVLink.mavlink_command_long_t req = new MAVLink.mavlink_command_long_t();

            req.target_system = 1;
            req.target_component = 1;

            req.command = (ushort)MAVLink.MAV_CMD.COMPONENT_ARM_DISARM;

            req.param1 = armed ? 0 : 1;
            armed = !armed;
            /*
            req.param2 = p2;
            req.param3 = p3;
            req.param4 = p4;
            req.param5 = p5;
            req.param6 = p6;
            req.param7 = p7;
            */

            byte[] packet = mavlink.GenerateMAVLinkPacket10(MAVLink.MAVLINK_MSG_ID.COMMAND_LONG, req);

            serialPort1.Write(packet, 0, packet.Length);

            try
            {
                var ack = readsomedata<MAVLink.mavlink_command_ack_t>(sysid, compid);
                if (ack.result == (byte)MAVLink.MAV_RESULT.ACCEPTED)
                {
                    
                }
            }
            catch
            {
            }
            armdiss.Text = armed.ToString();
        }
    }
 
}


