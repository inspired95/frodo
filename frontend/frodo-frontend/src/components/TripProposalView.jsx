import React from "react";
import TripProposalCard from "./TripProposalCard";

class TripProposalView extends React.Component {
    constructor(props) {
        super(props);
        this.state = {

        }
      }
      render(){
          
        var tripsProposalCards = this.props.tripsProposal.map(function(tripProposal){
            console.log(tripProposal);
            return <TripProposalCard tripProposal={tripProposal}/>;
          })

        return (
            <div className="tripsProposalContainer">
                
                <div >
                {tripsProposalCards}
                </div>
            </div>
        )
      }
}

export default TripProposalView;
