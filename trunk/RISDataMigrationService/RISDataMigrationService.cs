using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

using RIS.RISService.DataMigrators;

namespace RISDataMigrationService
{
    public partial class RISDataMigrationService : ServiceBase
    {
        private int interval = 30000;
        private System.Threading.Timer timer;
        public RISDataMigrationService()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("RISDataMigrationServiceLogSource"))
                System.Diagnostics.EventLog.CreateEventSource("RISDataMigrationServiceLogSource", "RISDataMigrationServiceLog");
            eventLog.Source = "RISDataMigrationServiceLogSource";
            eventLog.Log = "RISDataMigrationServiceLog";
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            eventLog.WriteEntry("RISDataMigration Service started.");
            TimerCallback timerDelegate = new TimerCallback(OnTick);
            timer = new System.Threading.Timer(timerDelegate, timer, 100, interval);
            //timer.Enabled = true;
            eventLog.WriteEntry("Timer enabled.");
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            eventLog.WriteEntry("RISDataMigration Service stoped.");
        }

        protected void OnTick(Object state)
        {
            eventLog.WriteEntry("Database sync started...");
            timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            
            try
            {
                RIS.RISService.DataMigrators.Debug.Instance.EventLog = eventLog;
                RISDataMigrator dataMigrator = new RISDataMigrator();
                dataMigrator.MigrateData();
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry("Exception in RISService:" + ex.ToString());
            }
            finally
            {
                timer.Change(interval, interval);
                eventLog.WriteEntry("Database sync ended");
            }
        }

    }
}
