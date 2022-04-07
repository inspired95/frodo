import React from "react";
import axios from 'axios';

class PlanTripForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tripPointsList: []
        }
      }

      componentDidMount() {
        axios.get(`http://localhost:5000/api/Stations`)
          .then(res => {
            console.log(res);
            const tripPoints = res.data;
            this.setState({tripPointsList: tripPoints });
          }).catch(function (error){
            console.log(error);
          })
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
            return <option key={point.name} label={point.coordinate.longitude + "," + point.coordinate.latitude} value={point.name}/>;
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