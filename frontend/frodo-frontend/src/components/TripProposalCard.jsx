import React from "react";
import axios from 'axios';
import TripProposalStage from "./TripProposalStage";
import { useNavigate } from 'react-router-dom';


function TripProposalCard(props) {

      const handleBuy = (guid) => {
        console.log("attempt to buy a ticket for guid: " + guid)
        try{
          
          axios.get(`http://localhost:5000/api/Ticket/Buy?journeyId=${guid}`, {
              headers: {
                  'Content-Type': 'application/json',
              }
          })
          .then(res => {
              console.log("buy a ticket successfull! ")
              // TODO show message
              

              redirectToTrip(guid);
              //this.props.history.push('/currentTrip/' + guid);

              //browserHistory.push('/currentTrip/' + guid)

          }).catch(function (error){
            console.error(error);
          })
        } catch(error) {
          console.error(error);
       }
      }

      const navigate = useNavigate();

      function redirectToTrip(guid) {
        navigate('/currentTrip/' + guid);
      }

      const guid = props.tripProposal.guid
        var tripsProposalStages = props.tripProposal.stages.map(function(stage){
            console.log(stage);
            return <TripProposalStage stage={stage}/>;
          })

    

          return(
            <div>
                <div class="card-body tripProposalCard">
                    <div class="card-header">
                        Trip option
                    </div>
                    {tripsProposalStages}

                    <button className="btn btn-primary">Show on a map</button>
                    <button className="btn btn-primary" onClick={() => handleBuy(guid)}>Buy a ticket</button>
                </div>
            </div>
          )
      
}


export default TripProposalCard;