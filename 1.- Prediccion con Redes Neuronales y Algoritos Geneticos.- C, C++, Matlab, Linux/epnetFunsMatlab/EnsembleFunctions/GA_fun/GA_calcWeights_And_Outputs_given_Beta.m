function [Pred, weights] = GA_calcWeights_And_Outputs_given_Beta(Pred, ...
    weights, populationNum, beta, Network, from)
    
% This fuction calculates the prediction given a value for Beta and return
% the weiths used to calculate the new prediction and the prediction (returned)

%prototype
%Input
    %Pred               variable where is saved the prediction
    %weights            variable where is save the weighs
    %populationNum      No ind in the population
    %beta               The beta needed to calculate the weights
    %Network            The Population
    %from               the set to obtain prediction and calculate the
                        %output of the ensemble:
                            %iteratePredI_norm or
                            %iteratePredF_norm
    
                            
                            
    sumBeta = 0;        
    for i=1:populationNum 
        sumBeta = sumBeta + exp(beta*(i));   %sum^{N}_{j=1}exp(beta*j)
    end
    for i=1:populationNum 
        weights(i) = (exp(beta*(populationNum + 1 - i))) / sumBeta;  %%w_{i} = (exp(beta*(N+1-i))) / (sum^{N}_{j=1}exp(beta*j))
    end
    

   
    %// the prediction is taken from iteratePredI_norm
    Net = ['Network{1,i}.',from];
    for i=1:populationNum
        Pred = Pred +  (weights(i) * eval(Net)); %1 in this moment only put the first line (1 pred)
    end