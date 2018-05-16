require_relative 'spec_helper'

describe Game do
  let(:game) { Game.new :name, :id, :year_of_release, :type }
  let(:creator) { game_creator }

  context '#initialize' do
    it 'initializes without errors' do
      expect { :creator }.not_to raise_error
    end
    it 'raises error when initialized with wrong arguments' do
      expect { Game.new('Super gra', '4', '2000', 'typ', 67) }.to raise_error(ArgumentError)
    end
    it 'creates new object without errors' do
      expect { game.to be_an_instance_of(Game) }
    end
    it 'returns name' do
      expect { game.name.to eq :name }
    end
    it 'returns id' do
      expect { game.id.to eq :id }
    end
    it 'returns year of realase' do
      expect { game.year_of_release.to eq :year_of_release }
    end
    it 'returns type' do
      expect { game.type.to eq :type }
    end
  end

  context '#users_get' do
    it 'returns an array type' do
      expect { game.users_get.to be_an(Array) }
    end
  end

  context '#to2_s' do
    let(:correct_result) { 'Nazwa: ' << creator.name.to_s << "\n" << 'Rok Wydania: ' << creator.year_of_release.to_s << "\n" << 'Typ : ' << creator.type.to_s << "\n" << 'Gracze: ' << "\n\n" }
    it 'returns a String type' do
      expect { game.to2_s.to be_an(String) }
    end
    it 'returns string in correct format' do
      expect { puts creator.to2_s }.to output(correct_result).to_stdout
    end
  end
end
