import React from "react";
import axios from 'axios';
import TripProposalStage from "./TripProposalStage";


class TripProposalCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {

        }
      }

      handleBuy(guid) {
        console.log("attempt to buy a ticket for guid: " + guid)

        const requestData = {journeyId:guid}
        try{
          
          axios.get(`http://localhost:5000/api/Ticket/Buy?journeyId=ef16699a-2c40-4fcd-bcec-ca7cb79d0dd8`, {
              headers: {
                  'Content-Type': 'application/json',
              }
          })
          .then(res => {
              console.log("buy a ticket response: " + res.data)
          }).catch(function (error){
            console.error(error);
          })
        } catch(error) {
          console.error(error);
       }
      }

      render(){
        const guid = this.props.tripProposal.GUID
        var tripsProposalStages = this.props.tripProposal.Stages.map(function(stage){
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
                    <button className="btn btn-primary" onClick={() => this.handleBuy(guid)}>Buy a ticket</button>
                </div>
            </div>
          )
      }
}


export default TripProposalCard;