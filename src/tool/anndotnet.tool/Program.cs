﻿using System;
using CNTK;
using NNetwork.Core;
using System.Threading;
using System.Linq;
using NNetwork.Core.Common;
using ANNdotNET.Lib.Ext;
using System.Collections.Generic;
using System.Globalization;

namespace anndotnet.core.app
{
    class Program
    {
        
        static void Main(string[] args)
        {
            transformDailyLevelVeanaLake();
            //return;
            //Iris flower recognition
            //Famous multi class classification datset: https://archive.ics.uci.edu/ml/datasets/iris
            //var mlConfigFile2 = "./model_mlconfigs/iris.mlconfig";
            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine($"****Iris flower recognition****");
            //Console.WriteLine(Environment.NewLine);
            //var token2 = new CancellationToken();
            //var result = MachineLearning.Run(mlConfigFile2, DeviceDescriptor.UseDefaultDevice(), token2, trainingProgress, null);
            ////evaluate model and export the result of testing
            //MachineLearning.EvaluateModel(mlConfigFile2, result.BestModelFile, DeviceDescriptor.UseDefaultDevice());

            //******run all configurations in the solution******
            //string strLocation1 = "C:\\sc\\github\\anndotnet\\src\\tool\\";
            //for (int i = 0; i < 10; i++)
            //    runAllml_configurations(strLocation1);

            runExample("Predict Solar Production",
               "C:\\Users\\bhrnjica\\OneDrive - BHRNJICA\\AI Projects\\ann-custom-models\\solar_production.mlconfig");

            //*****end of program*****
            Console.WriteLine("Press any key to continue!");
            Console.Read();

        }

        private static void transformDailyLevelVeanaLake()
        {
            var cnt = System.IO.File.ReadAllLines("C:\\sc\\vs\\Vrana\\VranaANN\\rawDataSets\\dailylevel-1978-2017.txt");
            var data = new List<List<object>>();
            var date = new DateTime(1978,1,1);
            foreach (var line in cnt)
            {
                
                var col = line.Split(new char[] {'\t' },StringSplitOptions.RemoveEmptyEntries);
                foreach(var c in col)
                {
                    var row = new List<object>();
                    row.Add(date);
                    row.Add(float.Parse(c));
                    data.Add(row);
                    date =date.AddDays(1);
                }

            }
            Func<DateTime, int> weekProjector =
    d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
         d,
         CalendarWeekRule.FirstFourDayWeek,
         DayOfWeek.Sunday);
            //daily data
            var weeklyData = data.GroupBy(x => new { year= DateTime.Parse(x[0].ToString()).Year,
                week = weekProjector(DateTime.Parse(x[0].ToString()))}).Select(x=> new {at1=x.Key.week,at2=x.Key.year,at3=x.Average(r=>float.Parse(r.ElementAt(1).ToString())) }).ToList();


            System.IO.File.WriteAllLines("C:\\sc\\vs\\Vrana\\VranaANN\\rawDataSets\\weeklyTimeserieslevel-1978-2017v.1.2.txt",
                weeklyData.Select(x => string.Join(";", x)));

        }

        

       

        private static void runAllml_configurations(string root)
        {
            runExample("Iris Flower Identification",
                $"{root}anndotnet.tool\\model_mlconfigs\\iris.mlconfig");

            runExample("Bezier Curve Machine Learning Demonstration",
                $"{root}anndotnet.tool\\model_mlconfigs\\BCML.mlconfig");

            runExample("Predict Daily Sales for 10 items",
                $"{root}anndotnet.tool\\model_mlconfigs\\daily_sales.mlconfig");

            runExample("Predict Solar Production",
                "C:\\Users\\bhrnjica\\OneDrive - BHRNJICA\\AI Projects\\ann-custom-models\\solar_production.mlconfig");

            runExample("Predict Future Sales",
                "C:\\Users\\bhrnjica\\OneDrive - BHRNJICA\\AI Projects\\ann-custom-models\\predict_future_sales_custom.mlconfig", CustomNNModels.CustomModelCallEntryPoint);

            runExample("Predict Future Sales",
                "C:\\Users\\bhrnjica\\OneDrive - BHRNJICA\\AI Projects\\ann-custom-models\\predict_future_sales.mlconfig");

            runExample("Titanic Survival",
                $"{root}anndotnet.wnd\\Resources\\Titanic\\TitanicProject\\DNNModel.mlconfig");
            runExample("Mushroms",
                $"{root}anndotnet.wnd\\Resources\\Mushroom\\MushroomProject\\TwoEmeddedLayers.mlconfig");

            runExample("Glass Identification",
                $"{root}anndotnet.wnd\\Resources\\Glass\\GlassIdentificationProject\\FeedForwardWithRandomDSGeneraton.mlconfig");

            runExample("Concrete SLump Test",
                $"{root}anndotnet.wnd\\Resources\\Concrete\\ConcreteSlumpProject\\FFNModel.mlconfig");

            runExample("Bike Sharing",
                $"{root}anndotnet.wnd\\Resources\\Bike\\BikeSharingProject\\DailySharingLSTM.mlconfig");

            runExample("Air Quality",
                $"{root}anndotnet.wnd\\Resources\\AirQ\\AirQuality\\Stacked LSTM config.mlconfig");

            runExample("MNIST Handwritting Digitc Recognition",
                           $"{root}anndotnet.wnd\\Resources\\MNIST\\MNIST-Project\\FFnet.mlconfig");

            runExample("Breast Cancer FF config",
                $"{root}anndotnet.wnd\\Resources\\BreastC\\BreastCancerProject\\FeedForward mlconfig.mlconfig");

            runExample("Breast Cancer Emb config",
                $"{root}anndotnet.wnd\\Resources\\BreastC\\BreastCancerProject\\CategoryEmbedding mlconfig.mlconfig");

            Console.WriteLine("Press Any Key To Continue.....");
            //Console.Read();
        }

        private static void runExample(string title, string mlConfigPath, CreateCustomModel model=null)
        {
            var mlConfigFile2 = mlConfigPath;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"****{title}****");
            Console.WriteLine(Environment.NewLine);
            var token2 = new CancellationToken();
            MachineLearning.Run(mlConfigFile2, DeviceDescriptor.UseDefaultDevice(), token2, trainingProgress, model);
        }

        static void trainingProgress(ProgressData progress)
        {
            //
            Console.WriteLine($"Epoch={progress.EpochCurrent} of {progress.EpochTotal};\t {progress.EvaluationFunName} for " +
                $"(Minibatch Data set = {progress.MinibatchAverageEval},Training Dataset = {progress.TrainEval}, Validation dataset = {progress.ValidationEval}");
        }


        static void RunExamples()
        {
            for(int i=0; i< 10; i++)
            {

            //Iris flower recognition
            //Famous multi class classification datset: https://archive.ics.uci.edu/ml/datasets/iris
            var mlConfigFile2 = "./model_mlconfigs/iris.mlconfig";
            Console.WriteLine(Environment.NewLine);
            Console.Title = $"****Iris flower recognition****";
            Console.WriteLine($"****Iris flower recognition****");
            Console.WriteLine(Environment.NewLine);
            var token2 = new CancellationToken();
            var result = MachineLearning.Run(mlConfigFile2, DeviceDescriptor.UseDefaultDevice(), token2, trainingProgress, null);
            MachineLearning.EvaluateModel(mlConfigFile2, result.BestModelFile, DeviceDescriptor.UseDefaultDevice());

            //Bezier Curve Machine Learning Demonstration
            //dataset taken form Code Project Article: 
            //https://www.codeproject.com/Articles/1256883/Bezier-Curve-Machine-Learning-Demonstration
            var mlConfigFile = "./model_mlconfigs/BCML.mlconfig";
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"****Bezier Curve Machine Learning Demonstration****");
            Console.WriteLine(Environment.NewLine);
            var token = new CancellationToken();
            MachineLearning.Run(mlConfigFile, DeviceDescriptor.UseDefaultDevice(), token, trainingProgress, null);

            //1. daily sales
            //modified dataset from preidct future sales
            var ds_mlConfigFile = "./model_mlconfigs/daily_sales.mlconfig";
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"****Predict Daily Sales for 10 items****");
            Console.WriteLine(Environment.NewLine);
            MachineLearning.Run(ds_mlConfigFile, DeviceDescriptor.UseDefaultDevice(), new CancellationToken(), trainingProgress, null);


            //1. solar production
            //CNTK Tutorial 106B_ https://cntk.ai/pythondocs/CNTK_106B_LSTM_Timeseries_with_IOT_Data.html
            var mlConfigFile11 = "C:\\Users\\bhrnjica\\OneDrive - BHRNJICA\\AI Projects\\ann-custom-models" +
                "\\solar_production.mlconfig";
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"****Predict Solar production****");
            Console.WriteLine(Environment.NewLine);
            var token11 = new CancellationToken();
            MachineLearning.Run(mlConfigFile11, DeviceDescriptor.UseDefaultDevice(), token11, trainingProgress, null);


            //2. Predict future sales,-  Multiple Input variables
            //Kaggle competition dataset
            var mlConfigFile1 = "C:\\Users\\bhrnjica\\OneDrive - BHRNJICA\\AI Projects\\ann-custom-models" +
                "\\predict_future_sales.mlconfig";
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"****Predict Future Sales****");
            Console.WriteLine(Environment.NewLine);
            var token1 = new CancellationToken();
            MachineLearning.Run(mlConfigFile1, DeviceDescriptor.UseDefaultDevice(), token1, trainingProgress, CustomNNModels.CustomModelCallEntryPoint);

            }
        }
    }
}
