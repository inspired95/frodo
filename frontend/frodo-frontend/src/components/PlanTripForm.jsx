import React from "react";

class PlanTripForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tripPointsList: ['Short Street', 'Long Street', 'Park', 'School']
        }
      }

      saveTripStartPoint = (event) => {
        event.preventDefault();
        var newTripStartPoint = event.target.tripStartPoint.value;
        console.log("saveTripStartPoint: " + newTripStartPoint);
        this.props.updateTripStartPointCallback(newTripStartPoint);
      }

      saveTripEndPoint = (event) => {
        event.preventDefault();
        var newTripEndPoint = event.target.tripEndPoint.value;
        console.log("saveTripEndPoint: " + newTripEndPoint);
        this.props.updateTripEndPointCallback(newTripEndPoint);
      }

      

      render(){
        var namesList = this.state.tripPointsList.map(function(point){
            return <option key={point} label={point} value={point}/>;
          })

        return (
            <div className="form-group planTripFormContainer">
                <form onSubmit={this.saveTripStartPoint} className="tripPointForm">
                    <label htmlFor="tripStartPoint">Start</label>
                    <input
                    className="form-control"
                        type="text"
                        name="tripStartPoint"
                        placeholder="Select trip start point from a map or select from a list"
                        defaultValue={this.props.tripStartPoint}
                        list='tripPointsList'
                    />
                    <datalist id='tripPointsList'>
                        { namesList }
                    </datalist>
                    <button type="submit" className="btn btn-primary tripPointFormBtn">
                        Save point
                    </button>
                </form>

                <form onSubmit={this.saveTripEndPoint} className="tripPointForm">
                    <label htmlFor="tripEndPoint">End</label>
                    <input
                    className="form-control"
                        type="text"
                        name="tripEndPoint"
                        placeholder="Select trip stop point from a map or select from a list"
                        defaultValue={this.props.tripEndPoint}
                        list='tripPointsList'
                    />
                    <datalist id='tripPointsList'>
                        { namesList }
                    </datalist>
                    <button type="submit" className="btn btn-primary tripPointFormBtn">
                        Save point
                    </button>
                </form>
            </div>
        )
      }
}

export default PlanTripForm;