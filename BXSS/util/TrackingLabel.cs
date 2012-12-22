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

        private ISettable _trackedControl;
        public ISettable TrackedControl
        {
            get { return _trackedControl; }
            set { ThrowIf.Null(value); _trackedControl = value; }
        }

        protected override void BeforeDraw()
        {
            base.BeforeDraw();

            Text = TrackedControl.Get();
        }
    }
}
