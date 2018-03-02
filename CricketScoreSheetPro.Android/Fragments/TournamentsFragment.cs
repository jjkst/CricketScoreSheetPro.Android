using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CricketScoreSheetPro.Android.Fragments
{
    public class TournamentsFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            SetHasOptionsMenu(true);
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.TournamentsView, container, false);

            //mSearchText = view.FindViewById<EditText>(Resource.Id.searchText);
            //mSearchText.TextChanged += SearchText_TextChanged;

            //batsmanAdapter = new BatsmanStatsAdapter(Players);
            //batsmanAdapter.ItemClick += OnItemClick;

            //mRecyclerViewBatsman = view.FindViewById<RecyclerView>(Resource.Id.batsmanStatsList);
            //mRecyclerViewBatsman.SetLayoutManager(new LinearLayoutManager(this.Activity));
            //mRecyclerViewBatsman.SetAdapter(batsmanAdapter);

            //var scrollview = view.FindViewById<ScrollView>(Resource.Id.batsmanStatsListScrollView);
            //scrollview.SmoothScrollingEnabled = true;
            //scrollview.SmoothScrollTo(0, 0);
            return view;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.searchText)
            {
                var s = (EditText)this.Activity.FindViewById(Resource.Id.searchTournament);
                if (s.Visibility == ViewStates.Gone)
                    s.Visibility = ViewStates.Visible;
                else s.Visibility = ViewStates.Gone;
            }
            return true;  
        }
    }
}