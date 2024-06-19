using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using RoomControllerCSharp.ViewModels;
using System.Diagnostics;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomControllerCSharp.Views;

public partial class MainView : UserControl
{
    public const string BridgeIP = "192.168.1.70";
    public const string ApiKey = "ZkroEghCIgkEd8FLuJLvzB2Sv3Zyv3jtjggosqUg";

    public static ILocalHueClient HueClient = null;

    public MainViewModel ViewModel;

    public MainView()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        this.DataContext = ViewModel;

        //ChangeTextButton.Click += Button_Click;
        ConnectButton.Click += ConnectButton_Click;
        SetAllLightsButton.Click += SetAllLightsButton_Click;
        SetSceneButton.Click += SetSceneButton_Click;
    }

    private async void RegisterAppButton_Click(object sender, RoutedEventArgs e)
    {
        ILocalHueClient hueClient = new LocalHueClient(BridgeIP);
        string? apiKey = await hueClient.RegisterAsync("HueControllerCS", "Desktop");
        Debug.WriteLine($"App key: {apiKey}");
    }

    private void ConnectButton_Click(object? sender, RoutedEventArgs e)
    {
        HueClient = new LocalHueClient(BridgeIP);
        HueClient.Initialize(ApiKey);
        //BridgeConnectionLabel.Content = "Bridge Connected!";
    }

    private async void GetAllLightsButton_Click(object? sender, RoutedEventArgs e)
    {
        if (HueClient == null)
        {
            Debug.WriteLine("!! Unable to get lights, bridge is not connected!");
            return;
        }

        var lights = (await HueClient.GetLightsAsync()).Where(x => x.State.IsReachable == true).ToList();

        if (lights == null)
        {
            Debug.WriteLine("!! Lights returned null!");
            return;
        }

        //LightListLabel.Content = $"Connected lights: {lights.Count}";
    }

    private async void SetAllLightsButton_Click(object? sender, RoutedEventArgs e)
    {
        if (HueClient == null)
        {
            Debug.WriteLine("!! Unable to get lights, bridge is not connected!");
            return;
        }

        var lights = (await HueClient.GetLightsAsync()).Where(x => x.State.IsReachable == true).ToList();

        if (lights == null)
        {
            Debug.WriteLine("!! Lights returned null!");
            return;
        }

        foreach (Light light in lights)
        {
            SetLightColor(light.Id, 0, 127, 254);
        }
    }

    private async Task SetLightColor(string lightId, int hue, int brightness, int saturation)
    {
        if (HueClient == null)
        {
            Debug.WriteLine("!! Unable to set light, bridge is not connected!");
            return;
        }

        var command = new LightCommand()
        {
            On = true,
            Brightness = (byte)brightness,
            Hue = hue,
            Saturation = (byte)saturation
        };

        // Send the command to the light
        await HueClient.SendCommandAsync(command, new List<string> { lightId });
        Debug.WriteLine($"Light {lightId} set to red with 50% brightness.");
    }

    private async void GetAllScenesButton_Click(object? sender, RoutedEventArgs e)
    {
        var scenes = await GetAllScenes();

        if (scenes == null)
        {
            Debug.WriteLine("!! scenes returned null!");
            return;
        }

        //SceneListLabel.Content = $"Available scenes: {scenes.Count}";

        foreach (Scene scene in scenes)
        {
            Debug.WriteLine($"Scene name: {scene.Name}, Id: {scene.Id}");
        }
    }

    private async Task<List<Scene>> GetAllScenes()
    {
        var scenes = await HueClient.GetScenesAsync();
        return scenes.ToList();
    }

    private async void SetSceneButton_Click(object? sender, RoutedEventArgs e)
    {
        var scenes = await GetAllScenes();

        if (scenes == null)
        {
            Debug.WriteLine("!! scenes returned null!");
            return;
        }

        //SceneListLabel.Content = $"Available scenes: {scenes.Count}";

        string? sceneId = scenes.FirstOrDefault()?.Id;

        if (!string.IsNullOrEmpty(sceneId))
        {
            await SetScene(sceneId);
        }
        else
        {
            Debug.WriteLine("!! No scenes available to set.");
        }
    }

    private async Task SetScene(string sceneId)
    {
        var groups = await HueClient.GetGroupsAsync();

        // Get the first group (normally index 0 is the default "All lights" group)
        var group = groups.FirstOrDefault();

        if (group == null)
        {
            Debug.WriteLine("!! Group was set to null!");
            return;
        }

        var command = new SceneCommand(sceneId);

        await HueClient.SendGroupCommandAsync(command, group.Id);
    }
}
