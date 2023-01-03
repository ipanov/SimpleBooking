import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as ResourcesStore from '../store/Resources';
import Modal from 'react-modal';
 

const customStyles = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-10%',
      transform: 'translate(-50%, -50%)',
      width: '70%',
      height: '50%'
    },
  };

// At runtime, Redux will merge together...
type ResourcesProps =  ResourcesStore.ResourcesState // ... state we've requested from the Redux store
    & typeof ResourcesStore.actionCreators // ... plus action creators we've requested

interface IState {
    showModal: boolean;
    selectedResource: ResourcesStore.Resource | null
}

let subtitle : HTMLHeadingElement | null;

class Resources extends React.PureComponent<ResourcesProps, IState>  {
    constructor (props: ResourcesProps) {
        super(props);
        this.state = {
          showModal: false,
          selectedResource: null
        };
        this.handleOpenModal = this.handleOpenModal.bind(this);
        this.handleCloseModal = this.handleCloseModal.bind(this);
      }
     
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public handleOpenModal (event: React.MouseEvent<HTMLButtonElement>, data: ResourcesStore.Resource)  {
        this.setState({ showModal: true, selectedResource: data   });
      }
      
    public handleCloseModal () {
        this.setState({ showModal: false });
      }

    public render() {
        return (
            <div>
                <React.Fragment>
                    <p id="tabelLabel">Resources</p>
                    {this.renderResourcesTable()}
                </React.Fragment>
                <Modal
                    isOpen={this.state.showModal}
                    onRequestClose={this.handleCloseModal}
                    style={customStyles}
                    contentLabel="Example Modal"
                >
                    <h2 ref={(_subtitle) => (subtitle = _subtitle)}>Booking {this.state.selectedResource?.name}</h2>
                    <button onClick={this.handleCloseModal}>x</button>
                    <div>I am a modal</div>
                    <form>
                    <input />
                        <button>tab navigation</button>
                        <button>stays</button>
                        <button>inside</button>
                        <button>the modal</button>
                    </form>
                </Modal>
            </div>
        );
    }

    private ensureDataFetched() {
        this.props.requestResources();
    }

    private renderResourcesTable() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.resources.map((resource: ResourcesStore.Resource) =>
                        <tr key={resource.id}>
                            <td>{resource.id}</td>
                            <td>{resource.name}</td>
                            <td><button onClick={(e) => this.handleOpenModal(e, resource)}>book here</button></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.resources, // Selects which state properties are merged into the component's props
    ResourcesStore.actionCreators // Selects which action creators are merged into the component's props
)(Resources as any);
