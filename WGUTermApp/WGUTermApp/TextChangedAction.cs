using System;
using Xamarin.Forms;
using WGUTermApp;

namespace TextChangeTrigger
{
    class TextChangedAction : TriggerAction<VisualElement>
    {
         public TextChangedAction()
        {

        }

        protected override  void Invoke(VisualElement visual)
        {
            App app = Application.Current as App;
            const string name = "Name";
            const string start = "Start";
            const string end = "End";
            const string status = "Status";
            const string instructor = "Instr";
            const string performance = "Perform";
            switch (visual.StyleId)
            {
                case name:
                    Editor editor = (Editor)visual;
                    app.Name = editor.Text;
                    break;
                case start:
                    DatePicker date = (DatePicker)visual;
                    app.StartDate = date.Date.ToString();
                    break;
                case end:
                    DatePicker date1 = (DatePicker)visual;
                    app.EndDate = date1.Date.ToString();
                    break;
                case status:
                    Picker picker = (Picker)visual;
                    app.Status = picker.SelectedItem.ToString();
                    break;
                case instructor:
                    Editor editor1 = (Editor)visual;
                    app.Instructor = editor1.ToString();
                    break;
                case performance:
                    Editor editor2 = (Editor)visual;
                    app.PerformanceAssessment = editor2.ToString();
                    break;
                default:
                    Editor editor3 = (Editor)visual;
                    app.ObjectiveAssessment = editor3.ToString();
                    break;
            }
            
        }
    }
}
