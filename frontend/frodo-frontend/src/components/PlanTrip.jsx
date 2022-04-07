import React from "react";
import PlanTripForm from "./PlanTripForm";

class PlanTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tripStartPoint: "",
            tripStopPoint: ""
        }
      }

      updateTripStartPoint = (newTripStartPoint) => {
        console.log("New TripStartPoint:" + newTripStartPoint);
        this.setState({ tripStartPoint: newTripStartPoint })
      }

      updateTripStopPoint = (newTripStoptPoint) => {
        console.log("New TripStopPoint:" + newTripStoptPoint);
        this.setState({ tripStopPoint: newTripStoptPoint })
      }


      render(){
        return (
            <div>
                <PlanTripForm updateTripStartPointCallback={this.updateTripStartPoint} updateTripStopPointCallback={this.updateTripStopPoint}/>
            </div>
        )
      }
}

export default PlanTrip;