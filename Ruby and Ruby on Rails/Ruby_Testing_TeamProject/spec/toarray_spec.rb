require_relative 'spec_helper'

describe ToArray do
  let(:path) { 'lib/csv/users.csv' }
  let(:path2) { 'lib/csv/games.csv' }
  let(:reader) { ToArray.new }
  let(:user) { User.new :login, :password, :date_of_birth, :token, :id }
  let(:auth_) { [] << user }

  context '#users_to_a' do
    it 'raises no errors' do
      expect { reader.users_to_a(path) }.to_not raise_error
    end
    it 'is an array type' do
      expect { reader.users_to_a(path).to be_a_kind_of(Array) }
    end
  end
  context '#games_to_a' do
    it 'does not raise an error' do
      expect { reader.games_to_a(auth_, path2) }.to_not raise_error
    end
    it 'is an array type' do
      expect { reader.games_to_a(auth_, path2).to be_a_kind_of(Array) }
    end
  end
end
