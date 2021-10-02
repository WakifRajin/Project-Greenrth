using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using System;

public class NotificationScript : MonoBehaviour
{

    public static NotificationScript instance;
    // Start is called before the first frame update
    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();


        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Reminder Notification",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

    }

    public void clearNot() {
        
    }

    public void sendNot(string tod, int time, int dayCount)
    {
        var notification = new AndroidNotification();
        notification.Title = "Greenrth";
        notification.Text = "Hey, good " + tod + "! It's time to water your plants";

        int hour = time / 100;
        int minute = time - hour * 100;

        if (dayCount == 0)
        {
            notification.FireTime = GetNextEvent(hour, minute);
        }
        else
        {
            notification.FireTime = GetNextEvent(hour, minute).AddDays(dayCount);
        }

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            if (dayCount != 0 && Info.instance.rainConditions.Contains(Info.instance.wInfo.weather[0].main))
            {
                AndroidNotificationCenter.SendNotification(notification, "channel_id");
            }
        }
    }

    // Update is called once per frame
    private DateTime GetNextEvent(int hour, int minute)
    {
        DateTime temp = DateTime.Now;

        temp = new DateTime(temp.Year, temp.Month, temp.Day,
        hour, minute, 0);

        return temp;

    }
}