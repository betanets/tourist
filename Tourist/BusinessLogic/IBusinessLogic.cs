namespace Tourist.BusinessLogic
{
    public interface IBusinessLogic
    {
        TouristDataSet ReadSight();
        TouristDataSet WriteSight(TouristDataSet dataSet);

        TouristDataSet ReadTour();
        TouristDataSet WriteTour(TouristDataSet dataSet);

        TouristDataSet ReadInstructor();
        TouristDataSet WriteInstructor(TouristDataSet dataSet);

        TouristDataSet ReadTourType();
        TouristDataSet WriteTourType(TouristDataSet dataSet);

        TouristDataSet ReadSchedule();
        TouristDataSet WriteSchedule(TouristDataSet dataSet);
    }
}
