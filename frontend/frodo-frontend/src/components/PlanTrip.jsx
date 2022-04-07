import React from "react";
import axios from 'axios';

import PlanTripForm from "./PlanTripForm";

class PlanTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tripStartPoint: "",
            tripEndPoint: "",
            tripsProposal: []
        }
        this.validatePlanTripBtn = this.validatePlanTripBtn.bind(this);
      }

      updateTripStartPoint = (newTripStartPoint) => {
        console.log("updateTripStartPoint: " + newTripStartPoint);
        this.setState({ tripStartPoint: newTripStartPoint });
        this.validatePlanTripBtn();
      }

      updateTripEndPoint = (newTripEndPoint) => {
        console.log("updateTripEndPoint: " + newTripEndPoint);
        this.setState({ tripEndPoint: newTripEndPoint });
        this.validatePlanTripBtn();
      }

      /*VALIDATION DOESN'T WORK*/
      validatePlanTripBtn = () => {
        var element = document.getElementById("planTripBtn");
          if(this.state.tripStartPoint === "" || this.state.tripEndPoint === ""){
            element.disabled = true;
          }else{
            element.disabled = false;
          }
      }

      sendPlanTripRequest = async( event ) => {
        event.preventDefault();

        const planTripFormData = new FormData();
        planTripFormData.append("tripStartPoint", this.state.tripStartPoint);
        planTripFormData.append("tripEndPoint", this.state.tripEndPoint);
        try {
            const response = await axios({
              method: "post",
              url: "http://localhost:8080/GetTripOptions",
              data: planTripFormData,
              headers: { "Content-Type": "multipart/form-data" },
            }).then((response) => {
                console.log(response);
                this.setState("tripsProposal", response.data.trips);
            });
          } catch(error) {
            console.log(error)
          }

      }


      render(){
          if(this.state.tripsProposal.length === 0){
            return (
                <div className="container-fluid">
                    <PlanTripForm tripStartPoint={this.state.tripStartPoint}  tripEndPoint={this.state.tripEndPoint} updateTripStartPointCallback={this.updateTripStartPoint} updateTripEndPointCallback={this.updateTripEndPoint}/>
                    <button id="planTripBtn" >Plan a trip</button>
                </div>
            )
          }else{
            return (
                <div className="container-fluid">

                </div>
            );
          }
      }
}

export default PlanTrip;