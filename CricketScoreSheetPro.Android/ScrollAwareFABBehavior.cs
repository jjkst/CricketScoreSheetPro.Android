using Android.Content;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;

namespace CricketScoreSheetPro.Android
{
    [Register("CricketScoreSheetPro.Android.ScrollAwareFABBehavior")]
    public class ScrollAwareFABBehavior : CoordinatorLayout.Behavior
    {
        public ScrollAwareFABBehavior(Context context, IAttributeSet attrs) : base()
        {
        }

        public override bool OnStartNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View directTargetChild, View target, int axes)
        {
            return axes == ViewCompat.ScrollAxisVertical || 
                base.OnStartNestedScroll(coordinatorLayout, child, directTargetChild, target, axes);
        }

        public override void OnNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed)
        {
            base.OnNestedScroll(coordinatorLayout, child, target, dxConsumed, dyConsumed, dxUnconsumed, dyUnconsumed);
            var fab = child.JavaCast<FloatingActionButton>();
            if (dyConsumed > 0 && fab.Visibility == ViewStates.Visible)
            {
                fab.Hide();
            }
            else if (dyConsumed < 0 && fab.Visibility != ViewStates.Visible)
            {
                fab.Show();
            }
        }
    }
}