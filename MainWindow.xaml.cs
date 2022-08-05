using Interface;
using Microsoft.ML;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string pathImage { get; set; }
        public string pathModel { get; set; }

        protected MLContext mlContext;
        protected DataViewSchema schema;
        protected ITransformer model;
        protected PredictionEngine<ModelInput, ModelOutput> engine;


        public MainWindow()
        {
            InitializeComponent();
            mlContext = new MLContext();
        }

        public List<string> Logic_Predict(string path)
        {                       
            ModelOutput transformation = engine.Predict(new ModelInput() { Label = "sample", ImageSource = path });
            double score = transformation.Score[0] * 100;

            score = Math.Round(score, 2);
            List<string> result = new List<string>() { transformation.Prediction, score.ToString() };
            return result;
        }

        private void MenuItem_Click_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    pathImage = ofd.FileName;
                    img1.Source = new BitmapImage(new Uri(pathImage));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_Info(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.Show();
        }

        private void Button_Click_Predict(object sender, RoutedEventArgs e)
        {
            List<string> res = Logic_Predict(pathImage);

            if (res[0] == "Parazite")
            {
                txtblcResult.Text = res[0];
                txtblcProcent.Text = res[1] + "%";
                txtblcResult.Foreground = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtblcResult.Text = "Uninfected";
                txtblcProcent.Text = res[1] + "%";
                txtblcResult.Foreground = System.Windows.Media.Brushes.Green;
            }
        }

        // добавление модели
        private void MenuItem_Click_AddModel(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".ZIP|*.zip";
            if (ofd.ShowDialog() == true)
            {
                pathModel = ofd.FileName;
                flag_Model.Background = System.Windows.Media.Brushes.Green;
                MessageBox.Show("Model added.");
            }
            FileStream file = File.OpenRead(pathModel); // выбираем путь
            model = mlContext.Model.Load(file, out schema);

            engine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);


        }
    }
}