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


      saveTripStartPoint = () => {
        var newTripStartPoint = document.getElementById("tripStartPointInput").value;
        console.log("saveTripStartPoint: " + newTripStartPoint);

        var foundPoint = this.state.tripPointsList.filter(function (el) {
            console.log(el.name);
            return el.name === newTripStartPoint;
          });

          if(foundPoint === null || foundPoint.length === 0){
            console.log("Point doesn't exist");
        }else{
            this.props.updateTripStartPointCallback(foundPoint[0].name, foundPoint[0].coordinate.latitude, foundPoint[0].coordinate.longitude);
        }
      }

      saveTripEndPoint = () => {
        var newTripEndPoint = document.getElementById("tripEndPointInput").value;
        console.log("saveTripEndPoint: " + newTripEndPoint);

        var foundPoint = this.state.tripPointsList.filter(function (el) {
            console.log(el.name);
            return el.name === newTripEndPoint;
          });
          if(foundPoint === null || foundPoint.length === 0){
            console.log("Point doesn't exist");
          }else{
            this.props.updateTripEndPointCallback(foundPoint[0].name, foundPoint[0].coordinate.latitude, foundPoint[0].coordinate.longitude);

          }

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
                    id="tripStartPointInput"
                    className="form-control"
                        type="text"
                        name="tripStartPoint"
                        placeholder="Select trip start point from a map or select from a list"
                        defaultValue={this.props.tripStartPoint.Name}
                        list='tripPointsList'
                        onChange={this.saveTripStartPoint}
                    />
                    <datalist id='tripPointsList'>
                        { namesList }
                    </datalist>
                </form>

                <form onSubmit={this.saveTripEndPoint} className="tripPointForm">
                    <label htmlFor="tripEndPoint">End</label>
                    <input
                    id="tripEndPointInput"
                    className="form-control"
                        type="text"
                        name="tripEndPoint"
                        placeholder="Select trip stop point from a map or select from a list"
                        defaultValue={this.props.tripEndPoint.Name}
                        list='tripPointsList'
                        onChange={this.saveTripEndPoint}
                    />
                    <datalist id='tripPointsList'>
                        { namesList }
                    </datalist>
                </form>
            </div>
        )
      }
}

export default PlanTripForm;