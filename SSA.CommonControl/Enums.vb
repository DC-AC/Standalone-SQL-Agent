Public Class Enums
    Public Enum JobStepAction
        Quit_With_Success = 1
        Quit_With_Failure = 2
        Go_To_Next_Step = 3
        Go_To_Step = 4
    End Enum
    Public Enum JobExecutionStatus
        Executing = 1
        Waiting_For_Thread = 2
        Between_Retries = 3
        Idle = 4
        Suspended = 5
        Performing_Completion_Actions = 7
    End Enum
    Public Enum e_freq_type
        One_Time_Only = 1
        Daily = 4
        Weekly = 8
        Monthly = 16
        Monthly_with_freq_interval = 32
        Run_At_Agent_Startup = 64
        Run_when_Idle = 128
    End Enum
    Public Enum e_freq_interval
        Once = 1
        Daily = 4
        Weekly = 8
        Monthly = 16
        Monthly_relative = 32
        Run_At_Agent_startup = 64
        Run_when_idle = 128
    End Enum
    Public Enum e_freq_subday_type
        Time_Specified = 1
        Seconds = 2
        Minutes = 4
        Hours = 8
    End Enum
    Public Enum e_freq_relative_interval
        Unused = 0
        First = 1
        Second = 2
        Third = 4
        Fourth = 8
        Last = 16
    End Enum
    Public Enum e_freq_interval_weekly
        Sunday = 1
        Monday = 2
        Tuesday = 4
        Wednesday = 8
        Thursday = 16
        Friday = 32
        Saturday = 64
    End Enum
    Public Enum e_freq_interval_monthly_relitave
        Sunday = 1
        Monday = 2
        Tuesday = 3
        Wednesday = 4
        Thursday = 5
        Friday = 6
        Saturday = 7
        Day = 8
        Weekday = 9
        WeekendDay = 10
    End Enum

End Class
