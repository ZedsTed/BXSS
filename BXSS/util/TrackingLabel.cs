namespace util
{
    public class TrackingLabel : Label
    {
        public TrackingLabel()
            : this(null)
        {
        }

        public TrackingLabel(ISettable trackedControl)
        {
            TrackedControl = trackedControl;
        }

        public ISettable TrackedControl { get; set; }

        protected override void BeforeDraw()
        {
            base.BeforeDraw();

            Text = TrackedControl.Get();
        }
    }
}
