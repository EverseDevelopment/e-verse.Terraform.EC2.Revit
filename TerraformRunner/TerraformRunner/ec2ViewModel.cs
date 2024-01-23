namespace TerraformRunner
{
    using System.Collections.ObjectModel;

    public class ec2ViewModel : ViewModelBase
    {
        private AMIObject selectedAmi;
        private ObservableCollection<AMIObject> amis;
        private SecurityObject selectedSecurity;
        private ObservableCollection<SecurityObject> securities;

        public ec2ViewModel()
        {
            this.Amis = new ObservableCollection<AMIObject>
            {
            };

            this.Securities = new ObservableCollection<SecurityObject>
            {
            };
        }

        public AMIObject SelectedAmi
        {
            get
            {
                return this.selectedAmi;
            }

            set
            {
                this.selectedAmi = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<AMIObject> Amis
        {
            get
            {
                return this.amis;
            }

            set
            {
                this.amis = value;
                this.OnPropertyChanged();
            }
        }

        public SecurityObject SelectedSecurity
        {
            get
            {
                return this.selectedSecurity;
            }

            set
            {
                this.selectedSecurity = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<SecurityObject> Securities
        {
            get
            {
                return this.securities;
            }

            set
            {
                this.securities = value;
                this.OnPropertyChanged();
            }
        }


    }
}
