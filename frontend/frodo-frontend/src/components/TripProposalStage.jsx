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
                        <p>
                            <span class="card-subtitle mb-2 text-muted">Comapany: </span>
                            <span class="card-text">{this.props.stage.meanOfTransportation}</span>
                        </p>
                        <p>
                            <span class="card-subtitle mb-2 text-muted">From: </span>
                            <span class="card-text">{this.props.stage.from.stopName}</span>
                            <div class="card-text">{this.props.stage.from.coordinates.latitude + ", " + this.props.stage.from.coordinates.longitude}</div>
                        </p>

                        <p>
                            <span class="card-subtitle mb-2 text-muted">To: </span>
                            <span class="card-text">{this.props.stage.to.stopName}</span>
                            <div class="card-text">{this.props.stage.to.coordinates.latitude + ", " + this.props.stage.to.coordinates.longitude}</div>
                        </p>

                        <p>
                        <span class="card-subtitle mb-2 text-muted">Starting time: </span>
                        <span class="card-text">{this.props.stage.startingTime}</span>
                        
                        </p>
                        <p>
                        <span class="card-subtitle mb-2 text-muted">Waiting time: </span>
                        <span class="card-text">{this.props.stage.waitingTime.days + "days " + this.props.stage.waitingTime.hours + "hours " + this.props.stage.waitingTime.minutes + "minutes"}</span>
                        </p>
                        <p>
                        <span class="card-subtitle mb-2 text-muted">Travel time: </span>
                        <span class="card-text">{this.props.stage.travelTime.days + "days " + this.props.stage.travelTime.hours + "hours " + this.props.stage.travelTime.minutes + "minutes"}</span>
                    </p>
                    <p>
                        <span class="card-subtitle mb-2 text-muted">Stage cost: </span>
                        <span class="card-text">{(Math.round(this.props.stage.price * 100) / 100).toFixed(2) + "$"}</span>
                        </p>
                    </div>
                </div>
            </div>
          )
        }
    }

export default TripProposalStage;