import React from "react";
import axios from 'axios';

import PlanTripForm from "./PlanTripForm";
import TripProposalView from "./TripProposalView";

import MapComponent from './Map';

class PlanTrip extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            tripStartPointName: "",
            tripStartPointLatitude: 0,
            tripStartPointLongitude: 0,
            tripEndPointName: "",
            tripEndPointLatitude: 0,
            tripEndPointLongitude: 0,
            tripsProposal: []
        }
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
        this.setState({ tripStartPointName: newTripStartPoint });
        this.setState({ tripStartPointLatitude: newTripStartPoint });
        this.setState({ tripStartPointLongitude: newTripStartPoint });
      }

      updateTripEndPoint = (newTripEndPoint) => {
        console.log("updateTripEndPoint: " + newTripEndPoint);
        this.setState({ tripEndPoint: newTripEndPoint });
      }

      sendPlanTripRequest = async(ref) => {
        console.log("sendPlanTripRequest")
        var today = new Date();

        var date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
        var requestData = JSON.stringify({
            "StartingPoint": {      
               "Longitude" : this.state.tripStartPointLatitude,       
               "Latitude" : this.state.tripStartPointLongitude      
            },      
            "EndingPoint" : {  
               "Longitude" : this.state.tripEndPointLatitude,   
               "Latitude" : this.state.tripEndPointLongitude    
            },     
            "StartingDate" : date
         });
         console.log(requestData);

         try{
            axios.get(`http://localhost:5000/api/JourneyPlanner`, requestData, {
                headers: {
                    'Content-Type': 'application/json',
                }
            })
            .then(res => {
                ref.setState({"tripsProposal": res.data});
            }).catch(function (error){
              console.log(error);
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
              ref.setState({"tripsProposal": trips});
            })
         }catch(error) {
            console.log(error);
         }
      }


      render(){
            return (
                <div className="container-fluid">
                    <PlanTripForm tripStartPoint={this.state.tripStartPoint}  tripEndPoint={this.state.tripEndPoint} updateTripStartPointCallback={this.updateTripStartPoint} updateTripEndPointCallback={this.updateTripEndPoint}/>
                    <button onClick={() => this.sendPlanTripRequest(this)} id="planTripBtn" >Plan a trip</button>
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