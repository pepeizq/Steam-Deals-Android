﻿using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Plugin.LocalNotifications;
using System;
using Xamarin.Forms;

namespace Steam_Deals_Android
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("modo"))
            {
                var modo = Application.Current.Properties["modo"] as string;

                if (modo == "0")
                {
                    wvPrincipal.Source = new Uri("https://pepeizqdeals.com/classic/?app=true");
                }
                else
                {
                    wvPrincipal.Source = new Uri("https://pepeizqdeals.com/?app=true");
                }
            }
            else
            {
                wvPrincipal.Source = new Uri("https://pepeizqdeals.com/?app=true");
            }

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "HtabeaAa9uFa0BN0uBQkBjy82MGcnhlVYVXjs1JM",
                BasePath = "https://pepeizq-s-deals-android.firebaseio.com"
            };

            IFirebaseClient cliente = new FirebaseClient(config);

            EventStreamResponse response = await cliente.OnAsync("mensajes/", (s, args, context) => {
                CrossLocalNotifications.Current.Show(args.Data, "test");
            });
        }

        private void WvPrincipal_Navigating(object sender, WebNavigatingEventArgs e)
        {
            bool exterior = true;

            if (e.Url == "https://pepeizqdeals.com/?app=true")
            {
                exterior = false;
            }

            if (e.Url == "https://pepeizqdeals.com/classic/?app=true")
            {
                exterior = false;
            }

            if (exterior == true)
            {
                e.Cancel = true;
                Device.OpenUri(new Uri(e.Url));
            }
        }

        private void BotonSteam_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://steamcommunity.com/groups/pepeizqdeals/"));
        }

        private void BotonTwitter_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://twitter.com/pepeizqdeals"));
        }

        private void BotonReddit_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://new.reddit.com/r/pepeizqdeals/new/"));
        }

        private void BotonMas_Clicked(object sender, EventArgs e)
        {
            gridMasOpciones.IsVisible = true;

            if (Application.Current.Properties.ContainsKey("modo"))
            {
                var modo = Application.Current.Properties["modo"] as string;
                
                if (modo == "0")
                {
                    botonMasOpcionesModoTexto.IsVisible = false;
                    botonMasOpcionesModoImagen.IsVisible = true;
                }
                else
                {
                    botonMasOpcionesModoTexto.IsVisible = true;
                    botonMasOpcionesModoImagen.IsVisible = false;
                }
            }
            else
            {
                botonMasOpcionesModoTexto.IsVisible = true;
            }
        }

        private void BotonMasOpcionesCerrar_Clicked(object sender, EventArgs e)
        {
            gridMasOpciones.IsVisible = false;
        }

        private void BotonMasOpcionesModoTexto_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["modo"] = "0";
            Application.Current.SavePropertiesAsync();

            gridMasOpciones.IsVisible = false;
            wvPrincipal.Source = new Uri("https://pepeizqdeals.com/classic/?app=true");
        }

        private void BotonMasOpcionesModoImagen_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["modo"] = "1";
            Application.Current.SavePropertiesAsync();

            gridMasOpciones.IsVisible = false;
            wvPrincipal.Source = new Uri("https://pepeizqdeals.com/?app=true");
        }
    }
}
