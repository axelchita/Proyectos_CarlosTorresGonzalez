function [ensembleBlank] = init_ensemble(var,populationNum)
    
ensembleBlank.Average = create_str_ensemble(var);
ensembleBlank.SRank_base_LCombination_05 = create_str_ensemble(var);
ensembleBlank.weightsRankBase_05  = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_1= create_str_ensemble(var);
ensembleBlank.weightsRankBase_1 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_15= create_str_ensemble(var);
ensembleBlank.weightsRankBase_15 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_2= create_str_ensemble(var);
ensembleBlank.weightsRankBase_2 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_25= create_str_ensemble(var);
ensembleBlank.weightsRankBase_25 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_3= create_str_ensemble(var);
ensembleBlank.weightsRankBase_3 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_35= create_str_ensemble(var);
ensembleBlank.weightsRankBase_35 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_4= create_str_ensemble(var);
ensembleBlank.weightsRankBase_4 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_45= create_str_ensemble(var);
ensembleBlank.weightsRankBase_45 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_5= create_str_ensemble(var);
ensembleBlank.weightsRankBase_5 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_55= create_str_ensemble(var);
ensembleBlank.weightsRankBase_55 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_6= create_str_ensemble(var);
ensembleBlank.weightsRankBase_6 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_65= create_str_ensemble(var);
ensembleBlank.weightsRankBase_65 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_7= create_str_ensemble(var);
ensembleBlank.weightsRankBase_7 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_75= create_str_ensemble(var);
ensembleBlank.weightsRankBase_75 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_8= create_str_ensemble(var);
ensembleBlank.weightsRankBase_8 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_85= create_str_ensemble(var);
ensembleBlank.weightsRankBase_85 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_9= create_str_ensemble(var);
ensembleBlank.weightsRankBase_9 = zeros(1,populationNum);
ensembleBlank.SRank_base_LCombination_95= create_str_ensemble(var);
ensembleBlank.weightsRankBase_95 = zeros(1,populationNum);
ensembleBlank.OutputBestSRBLC = 0;
ensembleBlank.SRLS_alg = create_str_ensemble(var);
ensembleBlank.GA = create_str_ensemble(var);
    