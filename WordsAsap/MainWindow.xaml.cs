using FirstFloor.ModernUI.Windows.Controls;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WordsAsap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {

        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        IScheduler m_scheduler;
        IJobDetail m_job;
        ModernDialog1 m_dialog;

        public MainWindow()
        {
            InitializeComponent(); 
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = string.Format("The {0} has been minimised. Click the tray icon to show it back.", ApplicationInfo.TitleFunc());
            m_notifyIcon.BalloonTipTitle = ApplicationInfo.TitleFunc();
            m_notifyIcon.Text = ApplicationInfo.TitleFunc();
            m_notifyIcon.Icon = new System.Drawing.Icon("acorn-7-16.ico");
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
            
           
            m_scheduler = StdSchedulerFactory.GetDefaultScheduler();
            
            // and start it off
            m_scheduler.Start();
            
            // define the job and tie it to our HelloJob class
        m_job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            m_scheduler.ScheduleJob(m_job, trigger);
            m_scheduler.PauseJob(m_job.Key);
        }

        void OnClose(object sender, CancelEventArgs args)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
            // and last shut down the scheduler when you are ready to close your program
            m_scheduler.Shutdown();
        }
        public delegate void NextPrimeDelegate();

        private WindowState m_storedWindowState = WindowState.Normal;
        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (m_notifyIcon != null)
                    m_notifyIcon.ShowBalloonTip(2000);


               // m_scheduler.ResumeJob(m_job.Key);

                Task.Factory.StartNew((x) =>
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new NextPrimeDelegate(() =>
                    {
                        m_dialog = new ModernDialog1();
                        m_dialog.WindowState = System.Windows.WindowState.Minimized;
                        m_dialog.ShowInTaskbar = false;
                        m_dialog.Topmost = true;
                    }));
                   
                   
                    while (true)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, new NextPrimeDelegate(() =>
                        {
                            m_dialog.MinWidth = 100;
                            m_dialog.ShowDialog();
                        }));
                        Thread.Sleep(10000);
                    }
                    
                } , TaskScheduler.FromCurrentSynchronizationContext());

                

            }
            else
            {
                m_storedWindowState = WindowState;
               // m_scheduler.PauseJob(m_job.Key);
            }
        }
        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }
    }

    public class HelloJob : IJob
    {
        
        public HelloJob()
        {            
           
        }
        public void Execute(IJobExecutionContext context)
        {
          // ModernDialog.ShowMessage("TODO: Displaye another word to learn","word asap", MessageBoxButton.OK);
           

        }
    }
}
