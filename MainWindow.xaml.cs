using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClimateObservationApp.Repositories;
using ClimateObservationApp.Model;
using ClimateObservationApp.ViewModels;

namespace ClimateObservationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        #region variables and objects
        ObservationRepository db = new ObservationRepository();
        Observer observer = new Observer();
        Observation observation = new Observation();
        Measurement measurement = new Measurement();
        List<Measurement> tempList = new List<Measurement>();
        List<ViewMeasurements> measurmentlist = new List<ViewMeasurements>();
        List<Listinterface> interfaceList = new List<Listinterface>();

        Observer selectedObserver;
        Category category1, category2, category3, selectedcategory, selectedcategorysub, selectedcategorysubsub;
        int observationID;
        #endregion    
        public MainWindow()
        {
            InitializeComponent();
            var category = db.GetCategories();
            comboCategory.ItemsSource = category;
            var areaDropdown = db.GetAreas();
            comboDropdown.ItemsSource = areaDropdown;
            btnUpdateObservation.Visibility = Visibility.Hidden;
            
        }
        
        /// <summary>
        /// Adds observer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            observer = new Observer

            {

                FirstName = firstName.Text,
                LastName = lastName.Text

            };
            observer = db.AddObserver(observer);
            UpdateObservers();
        }
        
        /// <summary>
        /// Update observers in listbox
        /// </summary>
        private void UpdateObservers ()
        {

            var observers = db.Getobservers();  
            lstBox.ItemsSource = null;
            lstBox.ItemsSource = observers;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            UpdateObservers();

        }
        /// <summary>
        ///Displays if observer was deleted
        /// </summary>
        /// <param name="check"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
       

        /// <summary>
        /// Deletes observer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            selectedObserver = (Observer)lstBox.SelectedItem;
            try { 
            //MessageBox.Show(selectedObserver.Id.ToString());
            bool hejobserver = db.deleteObserver(selectedObserver.Id);
            MessageBox.Show($"{selectedObserver.FirstName} {selectedObserver.LastName} is deleted");
            UpdateObservers();
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }
       /// <summary>
       /// Nulls comboboxes with button click
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NullComboBoxen();
        }

        #region Comboxes that displays categories
        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            selectedcategory = (Category)comboCategory.SelectedItem;
            if (selectedcategory == null)
                return;
            var category1 = db.GetSubCategories(selectedcategory.Id);    
            comboCategorySub.ItemsSource = null;
            comboCategorySub.ItemsSource = category1;

        }
        private void comboCategorySub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedcategorysub = (Category)comboCategorySub.SelectedItem;
            if (selectedcategorysub == null)
                return;
            var category2 = db.GetSubCategories(selectedcategorysub.Id);
            comboCategorySubSub.ItemsSource = null;
            comboCategorySubSub.ItemsSource = category2;
            //Gets Unit values  for textbox
            int unit_id = (int)selectedcategorysub.Unit_id;
            var unit_object = db.GetUnit(unit_id);
            lbl.Content = unit_object.Abbrevation;

        }
        private void comboCategorySubSub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedcategorysubsub = (Category)comboCategorySubSub.SelectedItem;
            if (selectedcategorysubsub == null)
                return;
            int unit_id = (int)selectedcategorysubsub.Unit_id;
            lbl.Content = db.GetUnit(unit_id);

        }
        #endregion

        /// <summary>
        /// Sends in observeration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCommitObservation_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            var observer1 = (Observer)lstBox.SelectedItem;
            observation = new Observation

            {

                Observer_Id = SelectedObserver(),
                Geolocation_Id = 0,
                Date = pickDate.SelectedDate.Value.Date

            };
            
            db.AddObservation(observation, tempList, observationID);
            //db.AddObs(observation, observationID);
          

            tempList.Clear();
            temperaryList.ItemsSource = null;
            MessageBox.Show($"Observation inskickad!");

            }

            catch (Exception ex1)
            {

                MessageBox.Show(ex1.Message);

            }
        }
         
       /// <summary>
       /// Nulls comboboxes
       /// </summary>
        private void NullComboBoxen()
    {

            comboCategorySub.ItemsSource = null;
            comboCategorySubSub.ItemsSource = null;
            var category = db.GetCategories();
            comboCategory.ItemsSource = null;
            comboCategory.ItemsSource = category;


        }


        /// <summary>
        /// Gets location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drpoDownArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //visa area namn, select vill skicka värde på geolocation id från geolocation med tillhörande area id 
            var selectedArea = (Area)comboDropdown.SelectedItem;
            int selectedAreaId = selectedArea.Id;
            //db.GetGeolocationID(selectedAreaId);
            observationID = db.GetGeolocationID(selectedAreaId).Id;
        }


        /// <summary>
        /// Updates observers observations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstBoxObservations.ItemsSource = null;
            lstBoxObservations.ItemsSource = db.GetObservations(SelectedObserver());

        }
        /// <summary>
        /// Gets measurments for observation
        /// </summary>
        private void GetMeasurments()
        {
            interfaceList.Clear();
            var selectedObservation = (Observation)lstBoxObservations.SelectedItem;
            if (selectedObservation == null)
                return;
            var observations = db.GetMeasurements(selectedObservation.Id);

            foreach (var o in observations)
            {
                var category = db.GetCategory(o.Category_id);
                interfaceList.Add(new Listinterface
                {
                    Measurement_id = o.Id,
                    Value = o.Value,
                    Category_name = category.Name,
                    Unit_name = db.GetUnit(category.Unit_id).Type
                }); ;
            }

            temperaryList.ItemsSource = null;
            temperaryList.ItemsSource = interfaceList;

        }

        private void lstBoxObservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetMeasurments();
        }

        /// <summary>
        /// Update measurment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateObservation_Click(object sender, RoutedEventArgs e)
        {
            
            var selectedMeasurement = (Listinterface)temperaryList.SelectedItem;
            double v = double.Parse(txtInput1.Text);
            db.UpdateMeasurment(selectedMeasurement.Measurement_id,v);
            GetMeasurments();
           
        }

        /// <summary>
        /// Hides Observation update button if no measurment is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void temperaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMeasurement = (Listinterface)temperaryList.SelectedItem;
            if (selectedMeasurement != null)
            {
                btnUpdateObservation.Visibility = Visibility.Visible;
            }
            else btnUpdateObservation.Visibility = Visibility.Hidden;

        }

      

        /// <summary>
        /// Adds measurment to observation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest1_Click(object sender, RoutedEventArgs e)
        {
           
            selectedcategorysub = (Category)comboCategorySub.SelectedItem;
            var category2 = db.GetSubCategories(selectedcategorysub.Id);
            float input = float.Parse(txtInput1.Text);

            measurmentlist.Add(new ViewMeasurements
            {
                
                Value = input,
                Category_name = IsChecked().ToString(),
                Unit_name = lbl.Content.ToString()
            });
            UpdateMeasurmentList();  

            tempList.Add(new Measurement
            {
                Value = input,
                Category_id = IsCheckedID()

            });

        }
        private void UpdateMeasurmentList()
        {
            temperaryList.ItemsSource = null;
            temperaryList.ItemsSource = measurmentlist;
        }


        #region Controls if comboboxes is checked to display correct information
        public Object IsChecked()
        {
            if (selectedcategorysubsub != null)

                return selectedcategorysubsub;
            else
                return selectedcategorysub;
        }

        public int IsCheckedID()
        {
            if (selectedcategorysubsub != null)

                return selectedcategorysubsub.Id;
            else
                return selectedcategorysub.Id;
        }
        #endregion


        /// <summary>
        /// Picks ID from the marked observer
        /// </summary>
        /// <returns></returns>
        public int SelectedObserver ()
        {
            var selected = (Observer)lstBox.SelectedItem;
            if (selected == null)
            return -2;
          
            int s = selected.Id;
            return s;
        }

    }
}