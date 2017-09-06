function [Pred, weights] = calcWeights_And_Outputs_given_Beta(Pred, weights, populationNum, beta, Network)
    
% This fuction calculates the prediction given a value for Beta and return
% the weiths used to calculate the new prediction and the prediction (returned)
    
    sumBeta = 0;        
    for i=1:populationNum 
        sumBeta = sumBeta + exp(beta*(i));   %sum^{N}_{j=1}exp(beta*j)
    end
    for i=1:populationNum 
        weights(i) = (exp(beta*(populationNum + 1 - i))) / sumBeta;  %%w_{i} = (exp(beta*(N+1-i))) / (sum^{N}_{j=1}exp(beta*j))
    end
    
%new weights, to take from the weights and not ranked..
%     %fitness = [0.2200000000000000,0.0300000000000000,0.100000000000000,0.130000000000000,0.500000000000000;]
% 
% fitness = 1./fitness
%     sumBeta = 0;
%    for i=1:populationNum  
%     sumBeta = sumBeta + fitness(1,i);
%    end
%     for i=1:populationNum 
%         weights(i) = fitness(1,i) / sumBeta;
%     end
    
    %sum(weights)
    
    
    
   
    %// the prediction is taken from iteratePredF_norm
    
    for i=1:populationNum
        Pred = Pred +  (weights(i) * Network{1,i}.iteratePredF); %1 in this moment only put the first line (1 pred)
    end