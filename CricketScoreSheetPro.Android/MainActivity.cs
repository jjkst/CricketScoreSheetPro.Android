﻿using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using CricketScoreSheetPro.Android.Fragments;
using Android.Views;
using System;

namespace CricketScoreSheetPro.Android
{
    [Activity(Label = "Cricket Score Sheet", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@drawable/ic_launcher"
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        private ActionBarDrawerToggle _drawerToggle;
        private DrawerLayout _drawerLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            // Initialize toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.ApplicationName);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.left_drawer);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);            
            _drawerToggle = new ActionBarDrawerToggle(this, _drawerLayout, toolbar,
                Resource.String.ApplicationName, Resource.String.ApplicationName);
            _drawerLayout.AddDrawerListener(_drawerToggle);
            _drawerToggle.SyncState();

            var prevFragment = FragmentManager.FindFragmentById(Resource.Id.content_frame);
            if (prevFragment == null)
            {
                var ft = FragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, new HomeFragment());
                ft.Commit();
            }
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var ft = FragmentManager.BeginTransaction();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_home):
                    SupportActionBar.SetTitle(Resource.String.ApplicationName);
                    ft.Replace(Resource.Id.content_frame, new HomeFragment(), nameof(HomeFragment));
                    FragmentManager.PopBackStackImmediate(null, PopBackStackFlags.Inclusive);
                    break;
                case (Resource.Id.nav_tournaments):                    
                    ft.Detach(FragmentManager.FindFragmentById(Resource.Id.content_frame));
                    ft.Replace(Resource.Id.content_frame, new TournamentsFragment(), nameof(TournamentsFragment));
                    break;
                    //case (Resource.Id.nav_batsmanstats):
                    //    SupportActionBar.SetTitle(Resource.String.BatsmanStats);
                    //    ft.Replace(Resource.Id.content_frame, new BatsmanStatsFragment());
                    //    break;
                    //case (Resource.Id.nav_bowlerstats):
                    //    SupportActionBar.SetTitle(Resource.String.BowlerStats);
                    //    ft.Replace(Resource.Id.content_frame, new BowlerStatsFragment());
                    //    break;
            }
            ft.Commit();
            _drawerLayout.CloseDrawers();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);           
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            menu.FindItem(Resource.Id.searchText).SetVisible(false);
            _drawerToggle.DrawerIndicatorEnabled = true;
            var currentfragment = FragmentManager.FindFragmentById(Resource.Id.content_frame);
            switch (currentfragment.Tag)
            {
                case nameof(TournamentsFragment):
                    SupportActionBar.SetTitle(Resource.String.TournamentsFragment);
                    menu.FindItem(Resource.Id.searchText).SetVisible(true);
                    break;
            }
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.home:
                    OnBackPressed();
                    return true;
                case Resource.Id.action_help:
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            if (FragmentManager.BackStackEntryCount != 0)
                FragmentManager.PopBackStack();
            else
                base.OnBackPressed();
        }

    }
}

