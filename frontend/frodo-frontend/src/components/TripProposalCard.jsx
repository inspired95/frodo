import React from "react";
import TripProposalStage from "./TripProposalStage";

class TripProposalCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {

        }
      }

      render(){
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
                    <button className="btn btn-primary">Buy a ticket</button>
                </div>
            </div>
          )
      }
}


export default TripProposalCard;