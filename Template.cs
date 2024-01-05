using KerbalWebProgram;
using KSP.Game;
using UnityEngine;
using Newtonsoft.Json;
using KSP.Sim.impl;



namespace TemplateAPI
{
    public static class ApiEndpoint
    {
        public static void init()
        {
            KWPmod.webAPI.Add("getTimeWarp", new getTimeWarp());
            KWPmod.webAPI.Add("setTimeWarp", new setTimeWarp());
            KWPmod.webAPI.Add("increaseTimeWarp", new increaseTimeWarp());
            KWPmod.webAPI.Add("decreaseTimeWarp", new decreaseTimeWarp());
            KWPmod.webAPI.Add("pauseTimeWarp", new pauseTimeWarp());
            KWPmod.webAPI.Add("unpauseTimeWarp", new unpauseTimeWarp());
            KWPmod.webAPI.Add("togglePauseTimeWarp", new togglePauseTimeWarp());
            KWPmod.webAPI.Add("getTelemetry", new getTelemetry());
            KWPmod.webAPI.Add("doControlInput2", new doControlInput2());
            //KSP.Game.GameManager.Instance.Game.ViewController.SetPause(bool);
            //KSP.Game.GameManager.Instance.Game.ViewController.IsPaused; //  bool - is time paused
        }
    }

    public class getTimeWarp : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        // KSP.Game.GameManager.Instance.Game.ViewController.TimeWarp.DecreaseTimeWarp();
        // KSP.Game.GameManager.Instance.Game.ViewController.TimeWarp.IncreaseTimeWarp();
        // KSP.Game.GameManager.Instance.Game.ViewController.TimeWarp.CurrentRate;
        public getTimeWarp()
        {
            parameters = new List<KWPParameterType> {};
            Type = "response";
            Name = "Get Time Warp";
            Description = "Get the current time warp rate";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            response.Data.Add("TimeWarp", GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex);
            return response;
        }
    }
    public class setTimeWarp : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        // KSP.Game.GameManager.Instance.Game.ViewController.TimeWarp.DecreaseTimeWarp();
        // KSP.Game.GameManager.Instance.Game.ViewController.TimeWarp.IncreaseTimeWarp();
        // KSP.Game.GameManager.Instance.Game.ViewController.TimeWarp.CurrentRate;
        public setTimeWarp()
        {
            parameters = new List<KWPParameterType> { new FloatParameter("warpRate", "The time warp rate index to set",true,0.0f,10.0f) };
            Type = "response";
            Name = "Set Time Warp";
            Description = "Set the current time warp rate";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            // put current warp rate in a variable
            int currentRateIndex = GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex;
            int currentWarp = currentRateIndex;
 //           {
 //               "ID": "51281", // random number
 //                   "Action": "setTimeWarp",
 //                   "parameters": {
 //                        "warpRate": 2.0
 //                    }
 //           }
            
            // requested index
            int tries = 0;
            while (currentWarp != (int)request.parameters["warpRate"] || tries > 10)
            {
                if (currentWarp > (int)request.parameters["warpRate"])
                {
                    GameManager.Instance.Game.ViewController.TimeWarp.DecreaseTimeWarp();
                    Debug.Log("Decreasing Time Warp");
                }
                else
                {
                    GameManager.Instance.Game.ViewController.TimeWarp.IncreaseTimeWarp();
                }
                tries++;
                currentWarp = GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex;
            }
            response.Data.Add("TimeWarp", GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex);
            return response;
        }
    }
    public class increaseTimeWarp : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        public increaseTimeWarp()
        {
            parameters = new List<KWPParameterType> { };
            Type = "response";
            Name = "Increase Time Warp";
            Description = "Increase the current time warp rate";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            GameManager.Instance.Game.ViewController.TimeWarp.IncreaseTimeWarp();
            response.Data.Add("TimeWarp", GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex);
            return response;
        }
    }
    public class decreaseTimeWarp : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        public decreaseTimeWarp()
        {
            parameters = new List<KWPParameterType> { };
            Type = "response";
            Name = "Decrease Time Warp";
            Description = "Decrease the current time warp rate";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            GameManager.Instance.Game.ViewController.TimeWarp.DecreaseTimeWarp();
            response.Data.Add("TimeWarp", GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex);
            return response;
        }
    }
    public class pauseTimeWarp : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        public pauseTimeWarp()
        {
            parameters = new List<KWPParameterType> { };
            Type = "response";
            Name = "Pause Time Warp";
            Description = "Pause the current time warp rate";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            // KSP.Game.GameManager.Instance.Game.ViewController.IsPaused; //  bool - is time paused
            GameManager.Instance.Game.ViewController.SetPause(true);
            response.Data.Add("TimeWarp", GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex);
            return response;
        }
    }
    public class unpauseTimeWarp : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        public unpauseTimeWarp()
        {
            parameters = new List<KWPParameterType> { };
            Type = "response";
            Name = "Unpause Time Warp";
            Description = "Unpause the current time warp rate";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            // KSP.Game.GameManager.Instance.Game.ViewController.IsPaused; //  bool - is time paused
            GameManager.Instance.Game.ViewController.SetPause(false);
            response.Data.Add("TimeWarp", GameManager.Instance.Game.ViewController.TimeWarp.CurrentRateIndex);
            return response;
        }
    }
    public class togglePauseTimeWarp : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        public togglePauseTimeWarp()
        {
            parameters = new List<KWPParameterType> { };
            Type = "response";
            Name = "Toggle Pause Time Warp";
            Description = "Toggle the current time warp rate";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            // KSP.Game.GameManager.Instance.Game.ViewController.IsPaused; //  bool - is time paused
            if (GameManager.Instance.Game.ViewController.IsPaused)
            {
                GameManager.Instance.Game.ViewController.SetPause(false);
            }
            else
            {
                GameManager.Instance.Game.ViewController.SetPause(true);
            }
            response.Data.Add("Paused State", GameManager.Instance.Game.ViewController.IsPaused);
            return response;
        }
    }

    public class getTelemetry : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }
        public getTelemetry()
        {
            parameters = new List<KWPParameterType> { };
            Type = "response";
            Name = "Get Telemetry";
            Description = "Get the current telemetry data";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel","Control","Time" };
        }

        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();
            // stringify with newton KSP.Game.GameManager.Instance.Game.ViewController.GetActiveVehicle() and log
            // KSP.Game.GameManager.Instance.Game.ViewController.GetActiveVehicle() // Vessel infomation in here
            VesselComponent vesselComponent = GameManager.Instance.Game.ViewController.GetActiveVehicle().GetSimVessel();
            response.Data.Add("Altitude From Radius", vesselComponent.AltitudeFromRadius);
            response.Data.Add("Altitude From Scenery", vesselComponent.AltitudeFromScenery);
            response.Data.Add("Altitude From Terrain", vesselComponent.AltitudeFromTerrain);
            response.Data.Add("Altitude From Sea Level", vesselComponent.AltitudeFromSeaLevel);
            response.Data.Add("Altitude From Surface", vesselComponent.AltitudeFromSurface);
            response.Data.Add("Atmospheric Temperature", vesselComponent.AtmosphericTemperature);
            response.Data.Add("Heading", vesselComponent.Heading);
            response.Data.Add("Latitude", vesselComponent.Latitude);
            response.Data.Add("Longitude", vesselComponent.Longitude);
            response.Data.Add("Mach Number", vesselComponent.MachNumber);
            response.Data.Add("G force", vesselComponent.geeForce);
            response.Data.Add("Pitch", vesselComponent.Pitch_HorizonRelative);
            response.Data.Add("Roll", vesselComponent.Roll_HorizonRelative);
            response.Data.Add("Yaw", vesselComponent.Yaw_HorizonRelative);
            response.Data.Add("Vertical Speed", vesselComponent.VerticalSrfSpeed);
            response.Data.Add("Horizontal Speed", vesselComponent.HorizontalSrfSpeed);
            response.Data.Add("Mission Time", vesselComponent.TimeSinceLaunch);
            response.Data.Add("Total forces", vesselComponent.PerturbationSmoothed.magnitude);
            response.Data.Add("Angular Velocity", vesselComponent.AngularVelocity.relativeAngularVelocity.vector);


            return response;
        }
    }
    public class doControlInput2 : KWPapi
    {
        public override List<KWPParameterType> parameters { get; set; }

        public override string Type { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public override string Author { get; set; }

        public override List<string> Tags { get; set; }


        public doControlInput2()
        {
            parameters = new List<KWPParameterType> { new FloatParameter("Pitch", "Pitch amount", false), new FloatParameter("Yaw", "Pitch amount", false), new FloatParameter("Roll", "Pitch amount", false) };
            Type = "response";
            Name = "do Control Input with trim";
            Description = "Allows you to send control inputs by overriding control trim";
            Author = "Vaanultra";
            Tags = new List<string> { "Vessel", "Control" };
        }
        public override ApiResponseData Run(ApiRequestData request)
        {
            ApiResponseData response = new ApiResponseData();
            response.ID = request.ID;
            response.Type = "response";
            response.Data = new Dictionary<string, object>();

            GameManager.Instance.Game.ViewController.TryGetActiveVehicle(out var vessel);
            var thisvessel = vessel as VesselVehicle;
            if (request.parameters.ContainsKey("Pitch"))
            {
                thisvessel.AtomicSet(null, null, null, null, null, null, null, request.parameters["Pitch"], null, null, null, null, null,
        null, null, null, null, null);
                response.Data.Add("Pitch", GameManager.Instance.Game.ViewController.GetActiveVehicle().GetSimVessel().Pitch_HorizonRelative);
            }
            if (request.parameters.ContainsKey("Yaw"))
            {
                thisvessel.AtomicSet(null, null, null, null, null, null, request.parameters["Yaw"], null, null, null, null, null, null,
        null, null, null, null, null);
                response.Data.Add("Yaw", GameManager.Instance.Game.ViewController.GetActiveVehicle().GetSimVessel().Yaw_HorizonRelative);
            }
            if (request.parameters.ContainsKey("Roll"))
            {
                thisvessel.AtomicSet(null, null, null, null, request.parameters["Roll"], null, null, null, null, null, null, null, null,
        null, null, null, null, null);
                response.Data.Add("Roll", GameManager.Instance.Game.ViewController.GetActiveVehicle().GetSimVessel().Roll_HorizonRelative);
            }

            return response;
        }
    }
}
// extend VesselComponent to include these doubles



// VesselVehicle.AltitudeFromRadius
// VesselVehicle.AltitudeFromScenery
// VesselVehicle.AltitudeFromTerrain
// VesselVehicle.AltitudeFromSeaLevel
// VesselVehicle.AltitudeFromSurface
// VesselVehicle.AtmosphericDensity
// VesselVehicle.AtmosphericDensityNormalized
// VesselVehicle.AtmosphericTemperature
// VesselVehicle.DynamicPressurekPa
// VesselVehicle.GeeForce
// VesselVehicle.Heading
// VesselVehicle.HorizontalSpeed
// VesselVehicle.Latitude
// VesselVehicle.Longitude
// VesselVehicle.MachNumber
// VesselVehicle.MainThrottle
// VesselVehicle.pitch
// VesselVehicle.roll
// VesselVehicle.yaw
// VesselVehicle.SurfaceSpeed
// VesselVehicle.VerticalSpeed