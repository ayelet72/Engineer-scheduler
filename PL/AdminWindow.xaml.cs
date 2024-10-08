﻿using PL.Engineer;
using PL.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

   

        public DateTime? selectedStartDateProject
        {
            get { return (DateTime?)GetValue(selectedStartDateProjectProperty); }
            set { SetValue(selectedStartDateProjectProperty, value); }
        }

        public static readonly DependencyProperty selectedStartDateProjectProperty =
            DependencyProperty.Register("selectedStartDateProject", typeof(DateTime?), typeof(AdminWindow), new PropertyMetadata(null));
        public Visibility IsScheduele

        {
            get { return (Visibility)GetValue(IsSchedueleProperty); }
            set { SetValue(IsSchedueleProperty, value); }
        }

        public static readonly DependencyProperty IsSchedueleProperty =
            DependencyProperty.Register("IsScheduele", typeof(Visibility), typeof(AdminWindow), new PropertyMetadata(null));


        public AdminWindow()
        {
            if (s_bl.StartProject != DateTime.MinValue)
                IsScheduele = Visibility.Hidden;
            else
                IsScheduele = Visibility.Visible;
            selectedStartDateProject = null;
            InitializeComponent();

        }
        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        { new EngineerListWindow().Show(); }
        private void btnTasks_Click(object sender, RoutedEventArgs e)
        { new TaskListWindow().Show(); }
        private void btnInitEngineers_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to init data?", "Yes", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Check the answer if the user
            if (result == MessageBoxResult.Yes)
            {
                s_bl.InitializeDB();
                MessageBox.Show($"initlization was successfully done", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
        private void btnResetEngineers_Click(object sender, RoutedEventArgs e)
        {
            s_bl.ResetDB();
            MessageBox.Show($"Reset was successfully done", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            IsScheduele = Visibility.Visible;

        }

        private void btnGant_Click(object sender, RoutedEventArgs e)
        {
            new GantWindow().Show();
        }

        private void SetStartDateProject_Click(object sender, RoutedEventArgs e)
        {
            if(selectedStartDateProject!=null)
            {
                s_bl.StartProject = selectedStartDateProject;

                s_bl.CreateAutomateSchedule(s_bl.StartProject);
                MessageBox.Show($"Project start date set to: {selectedStartDateProject}");
                IsScheduele = Visibility.Hidden;
            }
            
        }
     



    }
}

