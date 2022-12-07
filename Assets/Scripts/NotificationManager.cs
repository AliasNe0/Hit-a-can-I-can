using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    private readonly string CHANNEL_ID = "mgp_channel";

    public static NotificationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        var channel = new AndroidNotificationChannel()
        {
            Id = CHANNEL_ID,
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    //private void OnApplicationPause(bool pause)
    //{
    //    if (!pause) return;
    //    var notification = new AndroidNotification();
    //    notification.Title = "The game misses you!";
    //    notification.Text = "Come back please!";
    //    notification.FireTime = System.DateTime.Now.AddMinutes(0.5);

    //    AndroidNotificationCenter.CancelNotification((int)NotificationID.PauseNotification);
    //    AndroidNotificationCenter.SendNotificationWithExplicitID(notification, CHANNEL_ID, (int)NotificationID.PauseNotification);
    //}

    public void SendAchievementNotification(string achievementName)
    {
        var notification = new AndroidNotification();
        notification.Title = "New Achievement Unlocked";
        notification.Text = achievementName;
        notification.FireTime = System.DateTime.Now;

        AndroidNotificationCenter.CancelNotification((int)NotificationID.SettingsNotification);
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, CHANNEL_ID, (int)NotificationID.SettingsNotification);
    }
}
