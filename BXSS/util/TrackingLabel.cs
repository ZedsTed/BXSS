namespace util
{
    public class TrackingLabel : Label
    {
        public ISettable TrackedControl { get; set; }

        public TrackingLabel()
            : this(null)
        {
        }

        public TrackingLabel(ISettable trackedControl)
        {
            TrackedControl = trackedControl;
        }

        protected override void BeforeDraw()
        {
            base.BeforeDraw();

            Text = TrackedControl.Get();
        }
    }
}
