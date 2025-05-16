using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace lab7_main
{
    public partial class Form1 : Form
    {
        private Assembly hookAssembly;
        private Type hookType;
        private dynamic keyboardHook;
        private dynamic mouseHook;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadDllButton_Click(object sender, EventArgs e)
        {
            //if (!IsUserAdministrator())
            //{
            //    MessageBox.Show("Требуются права администратора", "Ошибка",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            try
            {
                string dllPath = @"D:\holn\Desktop\Study\ОС\lab7\GlobalEventHook\GlobalEventHook\bin\Debug\GlobalEventHook.dll";
                hookAssembly = Assembly.LoadFrom(dllPath);
                hookType = hookAssembly.GetType("GlobalEventHook.SystemEventHook");

                keyboardHook = Activator.CreateInstance(hookType);
                mouseHook = Activator.CreateInstance(hookType);

                var eventInfo = hookType.GetEvent("HookTriggered");
                var handler = Delegate.CreateDelegate(
                    eventInfo.EventHandlerType,
                    this,
                    GetType().GetMethod("OnSystemEvent"));

                eventInfo.AddEventHandler(keyboardHook, handler);
                eventInfo.AddEventHandler(mouseHook, handler);

                var startMethod = hookType.GetMethod("StartHook");
                var hookTypeEnum = hookAssembly.GetType("GlobalEventHook.SystemEventHook+HookType");

                startMethod.Invoke(keyboardHook, new[] { Enum.Parse(hookTypeEnum, "WH_KEYBOARD_LL") });
                startMethod.Invoke(mouseHook, new[] { Enum.Parse(hookTypeEnum, "WH_MOUSE_LL") });

                statusLabel.Text = "DLL загружена и хук крючьек взят рыбкой";
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Ошибка загрузки DLL: {ex.Message}";
                MessageBox.Show($"Не удалось загрузить DLL: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnSystemEvent(object sender, dynamic e)
        {
            try
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    if (e.KeyCode != null)
                    {
                        AddEventToLog($"Клавиатура: {(e.IsKeyDown == true ? "Нажата" : "Отпущена")} {e.KeyCode}");
                    }
                    else if (e.MousePosition != null)
                    {
                        var pos = (Point)e.MousePosition;
                        AddEventToLog($"Мышь: {e.MouseAction} [X: {pos.X}, Y: {pos.Y}]");
                    }
                }));
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    AddEventToLog($"Ошибка обработки: {ex.Message}");
                });
            }
        }

        private void AddEventToLog(string message)
        {
            if (listBoxEvents.Items.Count > 1000)
                listBoxEvents.Items.RemoveAt(0);

            //listBoxEvents.Items.Add($"{DateTime.Now:HH:mm:ss.fff} - {message}");
            listBoxEvents.Items.Add($"{message}");
            listBoxEvents.TopIndex = listBoxEvents.Items.Count - 1; // Автопрокрутка
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            keyboardHook?.Dispose();
            mouseHook?.Dispose();
            base.OnFormClosing(e);
        }

        //private static bool IsUserAdministrator()
        //{
        //    try
        //    {
        //        var identity = WindowsIdentity.GetCurrent();
        //        var principal = new WindowsPrincipal(identity);
        //        return principal.IsInRole(WindowsBuiltInRole.Administrator);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}