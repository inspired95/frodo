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
                        <h6 class="card-subtitle mb-2 text-muted">Comapany</h6>
                        <p class="card-text">{this.props.stage.meanOfTransportation}</p>

                        <h6 class="card-subtitle mb-2 text-muted">From</h6>
                        <p class="card-text">{this.props.stage.from.stopName}</p>
                        <p class="card-text">{this.props.stage.from.coordinates.latitude + ", " + this.props.stage.from.coordinates.longitude}</p>

                        <h6 class="card-subtitle mb-2 text-muted">To</h6>
                        <p class="card-text">{this.props.stage.to.stopName}</p>
                        <p class="card-text">{this.props.stage.to.coordinates.latitude + ", " + this.props.stage.to.coordinates.longitude}</p>

                        <h6 class="card-subtitle mb-2 text-muted">Starting time</h6>
                        <p class="card-text">{this.props.stage.startingTime}</p>
                        
                        <h6 class="card-subtitle mb-2 text-muted">Waiting time</h6>
                        <p class="card-text">{this.props.stage.waitingTime.days + "days " + this.props.stage.waitingTime.hours + "hours " + this.props.stage.waitingTime.minutes + "minutes"}</p>

                        <h6 class="card-subtitle mb-2 text-muted">Travel time</h6>
                        <p class="card-text">{this.props.stage.travelTime.days + "days " + this.props.stage.travelTime.hours + "hours " + this.props.stage.travelTime.minutes + "minutes"}</p>
                    
                        <h6 class="card-subtitle mb-2 text-muted">Stage cost</h6>
                        <p class="card-text">{(Math.round(this.props.stage.price * 100) / 100).toFixed(2) + "$"}</p>
                   
                    </div>
                </div>
            </div>
          )
        }
    }

export default TripProposalStage;