using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.App;

namespace CricketScoreSheetPro.Android.Fragments
{
    public class TournamentDetailFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TournamentDetailView, container, false);
            var val = Arguments;
            return view;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.home)
            {
                FragmentManager.PopBackStack();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}