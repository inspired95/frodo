import React from "react";

class TripProposalStage extends React.Component {
    constructor(props) {
        super(props);
        this.state = {

        }
      }

      render(){
          return(

            <div>
                <div class="card">
                    <div class="card-body">
                        <h6 class="card-subtitle mb-2 text-muted">Comapny</h6>
                        <p class="card-text">{this.props.stage.TransportCompanyId}</p>

                        <h6 class="card-subtitle mb-2 text-muted">From</h6>
                        <p class="card-text">{this.props.stage.From}</p>

                        <h6 class="card-subtitle mb-2 text-muted">To</h6>
                        <p class="card-text">{this.props.stage.To}</p>

                        <h6 class="card-subtitle mb-2 text-muted">Starting time</h6>
                        <p class="card-text">{this.props.stage.StartingTime}</p>
                        
                        <h6 class="card-subtitle mb-2 text-muted">Waiting time</h6>
                        <p class="card-text">{this.props.stage.WaitingTime}</p>

                        <h6 class="card-subtitle mb-2 text-muted">Travel time</h6>
                        <p class="card-text">{this.props.stage.TravelTime}</p>
                    </div>
                </div>
            </div>
          )
        }
    }

export default TripProposalStage;