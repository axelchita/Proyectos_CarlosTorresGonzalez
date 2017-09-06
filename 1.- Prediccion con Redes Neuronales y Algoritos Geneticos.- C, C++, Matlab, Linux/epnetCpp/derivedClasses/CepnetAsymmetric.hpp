/*!
 * CepnetAsymmetric.hpp
 *
 * This class inplement the Asymmettric EPNet algorithm
 *
 * Created: 18/08/10
 * modified at:
 * Class derived form the EPNet class
 */

#ifndef C_EPNETASYMMETRIC_HPP
#define C_EPNETASYMMETRIC_HPP



class C_epnetAsymmetric: public C_EPNet{
public:
	// methods

	//Constructor and destructor
	C_epnetAsymmetric( char fileRun[] ) :  C_EPNet( fileRun ){

	}
	~C_epnetAsymmetric(){ 	}

	// this is a virtual function in class EPNet
	void startEPNet(void);

	int epnetAddTournament(int );									// virtual calss in EPNet class, add IHC only here cause delays are not used
																							// in the assimetric case


};// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



void C_epnetAsymmetric::startEPNet(void){
	try{

		// local variables ////////////////////////////////


		// to stop the algorithm
		double *previousFit = NULL;
		double *previousClassE = NULL;
		previousFit = allocate(previousFit, 1);
		previousClassE = allocate(previousClassE, 1);
		previousFit[0] = 100.0;
		previousClassE[0] = 100.0;
		// ----------------------------------------------------------

		int selected;
		//double **probability = NULL;
		double initTemp = temperature;					// temperature is a global variable
		int winner = -1;


		/////////////////////  PARTIAL TRAINING  //////////////////////////
		cout << "- - - - start Asymmetric EPNet main -- first training - before enter to the main loop of the epnet" << endl;

		if(trainMultipleSets == ON)
			trainPopMultiScales(TRAIN_INSIDE,&scaleNet[0]);
		else
			trainPop(TRAIN_INSIDE);

		cout << " - - - - '''' End '''' first training - - - - - - before enter epnet" << endl;

		//cout << "Before Rank.........." << endl; EPNet.printPop();
		///////////////  Rank the population  /////////////////////////
		rankk();					        //cout << "After Rank.........." << endl;            EPNet.printPop();

		// CALCULATE THE PROBABILITY
		// Use the rank base to obtain the probabilities
		//obtainProb(probability);

		Generation = 0;
		while(Generation < expectedGenerations){
			cout << "__________________________________________________" << endl;
			cout << endl << "+ + + + Main Loop EPNet, Generation = " << Generation << " + + + + + + " << endl << endl;     		cout << "-------------------------------------------------------------------------------" << endl;


			/////   Select one individual accordingly its probability  /////
			//selected = selection(probability);
			selected = (int ) (populationNum * rando());
			if(MYDEBUG_EPNET == 1)         	cout << "Individual Selected = " << selected << endl << "Status =" << Ppopulation[selected]->status[0] << endl << "Fitness = " << Ppopulation[selected]->fitness[0] << endl;


			////////// STEP 6 EPNET ALG ///////////////////////////////////
			/////////  Modified Backpropagation MBP //////////////////////
			if(Ppopulation[selected]->status[0] == SUCCESS &&   (baldwin == OFF)){
				epnetTrainMBP(selected);
			}
			else{
				///////// STEP 7 //////////////////////////////////////////////
				// Train with Simulate Annealing (SA) ///////////////////////
				if (baldwin == OFF)
					epnetTrainSA(selected, initTemp);

				// Evaluation for the SA
				if( ( (net1->fitness[0]*100) / Ppopulation[selected]->fitness[0])  <= STP   &&   net1->fitness[0] > 0   &&   useSA == ON &&  (baldwin == OFF)){
					Ppopulation[selected]->copyNet(net1);
					Ppopulation[selected]->status[0] = SUCCESS;
					mutHT ++;
					mutSA ++;
					if (MYDEBUG_EPNET == 1) 				cout << "Replace Selected individual by Network trainied with SA " << endl;
				}
				////////// STEP 8 //////////////////////////////////////////////////////////////////////
				/// Delete nodes ///////////////////////////////////////////////////
			/*	else{
					if(MYDEBUG_EPNET == 1) {        	    	cout << endl << "- - - Delete (Input or hidden Node) - - -" << endl;         	    	cout << "Fitness before enter in Delete function = " << Ppopulation[selected]->fitness[0] << endl; }

					//Copy Networks
					net2->copyCleanNet(Ppopulation[selected]);

					//Delete Nodes or inputs delays and hidden nodes and compete to survive
					if(fixedinputs == FIXED)
						winner = net2->deleteNode(&scaleNet[0]);
					else
						winner = emptyNet->deleteIHAssymetric(&net2, &scaleNet[0]);

					if(MYDEBUG_EPNET == 1){        	    	cout << "Fitness after enter in Delete Node/Input/Delays = " << net2->fitness[0] << endl;         	    	cout << " Fitness last = " << Ppopulation[populationNum-1]->fitness[0] << endl; }

					if( (winner > 0) && (net2->fitness[0] < Ppopulation[populationNum-1]->fitness[0])){
						Ppopulation[populationNum-1]->copyCleanNet(net2);
						if (MYDEBUG_EPNET == 1)  cout << "Last network replaced by function deleteNode" << endl << endl;
					}
					////////// STEP 9 ////////////////////////////////////////////////////////////////////
					// Delete Connections ////////////////////////////////////////////////////////////////
					else{
						winner = epnetDelConn(selected);

						if((winner > 0) && (net2->fitness[0] < Ppopulation[populationNum-1]->fitness[0])){
							Ppopulation[populationNum-1]->copyCleanNet(net2);
							if (MYDEBUG_EPNET == 1)  cout << "Last network replaced by function delete connenctions, check ahead to know if the network was mutated or not"<< endl << endl;
						}
						*/
				/// Delete nodes ///////////////////////////////////////////////////
								else{
									winner = epnetDelNodes(selected);

									if( (winner > 0) && (net2->fitness[0] < Ppopulation[populationNum-1]->fitness[0])){
										Ppopulation[populationNum-1]->copyCleanNet(net2);
										if (MYDEBUG_EPNET == 1)  cout << "Last network replaced by function deleteNode" << endl << endl;
									}
									////////// STEP 9 ////////////////////////////////////////////////////////////////////
									// Delete Connections ////////////////////////////////////////////////////////////////
									else{
										winner = epnetDelConn(selected);

										if((winner > 0) && (net2->fitness[0] < Ppopulation[populationNum-1]->fitness[0])){
											Ppopulation[populationNum-1]->copyCleanNet(net2);
											if (MYDEBUG_EPNET == 1)  cout << "Last network replaced by function delete connenctions, check ahead to know if the network was mutated or not"<< endl << endl;
										}
										////////// STEP 10 ////////////////////////////////////////////////////////////////////////////////
										//// delete inputs
										else{
											winner = epnetDelInputs(selected);

											if( winner > 0  && (net2->fitness[0] < Ppopulation[populationNum-1]->fitness[0])){
												Ppopulation[populationNum-1]->copyCleanNet(net2);
												if (MYDEBUG_EPNET == 1)  cout << "Last network replaced by function delete input"<< endl << endl;
											}
						////////// STEP 10 ////////////////////////////////////////////////////////////////////////////////
						////////////////////////////////////////////////////////////////////
						//// Add nodes and connections delays or inputs /////////
						else{
							winner = epnetAddTournament(selected);
						}			 //end additons	/////////////////////
					} 					  //end delete connections  /////
				}						 //end delete node			 ////
								}
			}							//end SA							///
			// Here the mutation have finished 	/////////////

				// call a function to update the correct mutation used
				updateMutation(winner);

				//rank the population that have been modified
				rankk();

				//save all the information needed in the variables per generation
				saveAllparam(Generation,selected);



				// Stopping criteria
				if ( stopEPNet(Generation, previousFit,  Ppopulation[0]->fitness[0], previousClassE, Ppopulation[0]->predictI->classifError )  ){
					Generation++;
					break;
				}




				Generation++;
			}//////////////////////////////////////////////////////////////////
			///////////////////////   END EPNET    ////////////////////////////
			///////////////////////////////////////////////////////////////////


			/////// All the individual in the last generations ////////////////
			///////      are trained with the last test set    ////////////////

			//Only to be shure that the best network is in the first position
			//if not, in this moment do not care, implication, lost one good solution
			// check later

			/////////////////////  FINAL TRAINING  //////////////////////////
			//cout << "Fitness before final training" << endl; EPNet.printPopFit();
			if(trainMultipleSets == ON)
				trainPopMultiScales(TRAIN_OUTSIDE,&scaleNet[0]);
			else
				trainPop(TRAIN_OUTSIDE);

			//trainPopFinal();//(&scaleNet[0]); 												// this one restart the weights and the train

			//rank the population again
			rankk();       	//cout << "Fitness after rankk" << endl; EPNet.printPopFit();

			// run the ensemble
			if( RUN_ENSEMBLE == 1){
				//////// New version of the code - ENSEMBLE method ////////////////
				cout << endl << "Ensemble method " << endl;
				//Average
				//averageEnsemble();

				//RBLC
				//RBLCEnsemble();
			}


			// liberate memory
			safefree( &previousFit);
			safefree( &previousClassE);
		}
		catch (...) {
			cout << "Something were wrong in the EPNet hierarquical class" << endl;
			processException();
			exit(EXIT_FAILURE);    // exit main() with error status
		} // end try
}// End Assimetric algorithm ///////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////
/////////////////////////////
///////////
///



int C_epnetAsymmetric::epnetAddTournament(int indSelected){
	int Winner = -1;

	// The winner replace the last network, no matter its performance
	if(MYDEBUG_EPNET == 1){ 				cout << endl << " %%%%% - - Add connection / node /  Input (Assimetric algorithm)- %%%%%" << endl;      	            	cout << "Fitness before enter to add node / con = " << Ppopulation[indSelected]->fitness[0] << endl; }

	// Copy networks, allocating memory
	net2->copyCleanNet(Ppopulation[indSelected]);

	// Call add Nodes / connections
	if(fixedinputs == FIXED)
		Winner = emptyNet->addNodeCon(&net2, &vecNet[0], &scaleNet[0]); // pas the addres of the net allow to modify the pointer to it and return it automatically
	else																								// useful if the size of the net changes inside the function
		Winner = emptyNet->addIHCAssymetric(&net2,&vecNet[0], &scaleNet[0]);

	if( Winner > 0 ){
		if (MYDEBUG_EPNET == 1)  cout << "Last network replaced by function addNodeCon or  addIDHC, check ahead to now more"<< endl << endl;
		Ppopulation[populationNum-1]->copyCleanNet(net2);
	}

	return Winner;
}


#endif
