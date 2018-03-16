using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using System;

namespace CricketScoreSheetPro.Android
{
    public class XamarinRecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        public event LoadMoreEventHandler LoadMoreEvent;
        public FloatingActionButton FloatingButton { get; set; }
        private LinearLayoutManager LayoutManager;

        public XamarinRecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            LayoutManager = layoutManager;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            if (dy > 0 && FloatingButton.Visibility == ViewStates.Visible)
            {
                FloatingButton.Hide();
            }
            else if (dy < 0 && FloatingButton.Visibility != ViewStates.Visible)
            {
                FloatingButton.Show();
            }

            //var visibleItemCount = recyclerView.ChildCount;
            //var totalItemCount = recyclerView.GetAdapter().ItemCount;
            //var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();

            //if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
            //{
            //    LoadMoreEvent(this, null);
            //}
        }
    }
}