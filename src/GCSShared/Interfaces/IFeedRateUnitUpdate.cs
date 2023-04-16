namespace GSendShared
{
    public interface IFeedRateUnitUpdate
    {
        FeedRateDisplayUnits FeedRateDisplay { get; set; }

        void UpdateFeedRateDisplay();
    }
}
