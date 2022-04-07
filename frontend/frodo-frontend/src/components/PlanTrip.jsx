import React from "react";
import axios from 'axios';

import PlanTripForm from "./PlanTripForm";
import TripProposalView from "./TripProposalView";

import MapComponent from './Map';

class PlanTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tripStartPoint: "",
            tripEndPoint: "",
            tripsProposal: []
        }
        this.validatePlanTripBtn = this.validatePlanTripBtn.bind(this);
        this.sendPlanTripRequest = this.sendPlanTripRequest.bind(this);
        this.updateTripStartPoint = this.updateTripStartPoint.bind(this);
      }

      handleStartSelected(coords) {
        this.updateTripStartPoint(coords.lat + ", " + coords.lng)
      }

      handleEndSelected(coords) {
        this.updateTripEndPoint(coords.lat + ", " + coords.lng)
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

      //NOT TO USE
      localizeMe = (event) => {
        event.preventDefault();
        if (navigator.geolocation) {
            navigator.geolocation.watchPosition(function(position) {

              console.log("Latitude is :", position.coords.latitude);
              console.log("Longitude is :", position.coords.longitude);
              var coords = "" + position.coords.latitude+","+ position.coords.longitude;
              console.log(coords);
              //updateTripStartPoint(coords);
            });

          }
      }




      sendPlanTripRequest = async( event ) => {
        event.preventDefault();
        console.log("sendPlanTripRequest")

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
            var trips = { 
                GUID : 524242342,
                "trips" : [
                  {
                    "GUID": 1,
                    "Stages" : [
                        {
                            "TransportCompanyId":"TransportCompanyId1",
                            "From": "PointA",
                            "To": "PointB",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"20",
                            "TravelTime": "21",
                        },
                        {
                            "TransportCompanyId":"TransportCompanyId2",
                            "From": "PointB",
                            "To": "PointC",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"22",
                            "TravelTime": "23",
                        },
                        {
                            "TransportCompanyId":"TransportCompanyId3",
                            "From": "PointC",
                            "To": "PointD",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"24",
                            "TravelTime": "25",
                        }
                    ],
                  },
                  {
                    "GUID": 2,
                    "Stages" : [
                        {
                            "TransportCompanyId":"TransportCompanyId1",
                            "From": "PointA",
                            "To": "PointB",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"20",
                            "TravelTime": "21",
                        },
                        {
                            "TransportCompanyId":"TransportCompanyId2",
                            "From": "PointB",
                            "To": "PointC",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"22",
                            "TravelTime": "23",
                        },
                        {
                            "TransportCompanyId":"TransportCompanyId3",
                            "From": "PointC",
                            "To": "PointD",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"24",
                            "TravelTime": "25",
                        }
                    ],
                  },
                  {
                    "GUID": 1,
                    "Stages" : [
                        {
                            "TransportCompanyId":"TransportCompanyId1",
                            "From": "PointA",
                            "To": "PointB",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"20",
                            "TravelTime": "21",
                        },
                        {
                            "TransportCompanyId":"TransportCompanyId2",
                            "From": "PointB",
                            "To": "PointC",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"22",
                            "TravelTime": "23",
                        },
                        {
                            "TransportCompanyId":"TransportCompanyId3",
                            "From": "PointC",
                            "To": "PointD",
                            "StartingTime": "2017-09-08T19:01:55.714942+03:00",
                            "WaitingTime":"24",
                            "TravelTime": "25",
                        }
                    ],
                  }
                ]
              };
              console.log(JSON.stringify(trips, null, 2))
              this.setState({"tripsProposal": trips});
          }

      }


      render(){
            return (
                <div className="container-fluid">
                    <PlanTripForm tripStartPoint={this.state.tripStartPoint}  tripEndPoint={this.state.tripEndPoint} updateTripStartPointCallback={this.updateTripStartPoint} updateTripEndPointCallback={this.updateTripEndPoint}/>
                    <button onClick={this.sendPlanTripRequest} id="planTripBtn" >Plan a trip</button>
                    { this.state.tripsProposal.length !== 0 &&
                        <TripProposalView tripsProposal={this.state.tripsProposal}/>
                    }

                    <MapComponent onStartSelected={(coords) => this.handleStartSelected(coords)}
                    onEndSelected={(coords) => this.handleEndSelected(coords)}/>
                </div>
            )
      }
}

export default PlanTrip;