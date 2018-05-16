require_relative 'spec_helper'
require 'pp'

describe User do
  let(:user) { User.new :login, :password, :date_of_birth, :token, :id }
  let(:users) { reader.users_to_a(path) }

  context '#initialize' do
    let(:test_user) { user_creator }
    it 'creates new object without errors' do
      expect { :user.to be_an_instance_of(User) }
    end
    it 'initializes without errors' do
      expect { :test_user.not_to raise_error }
    end
    it 'initializes with wrong number of arguments raise an error' do
      expect { User.new('log', 'pas', 2).to raise_error(ArgumentError) }
    end
    it 'returns login properly' do
      expect { user.login.to eq :login }
    end
    it 'returns password properly' do
      expect { user.password.to eq :password }
    end
    it 'returns year of birth properly' do
      expect { user.date_of_birth.to eq :date_of_birth }
    end
    it 'returns year of death properly' do
      expect { user.token.to eq :token }
    end
    it 'returns id properly' do
      expect { user.id.to eq :id }
    end
  end

  context '#to_s' do
    let(:creator) { user_creator }
    let(:correct_result_part1) { 'Nick: ' + creator.login.to_s + "\n" << 'Rok urodzenia: ' }
    let(:correct_result_part2) { creator.date_of_birth.to_s + "\n" + 'Token: ' + creator.token.to_s + "\n" + 'Wiek: ' }
    let(:correct_result_part3) { (2018 - creator.date_of_birth.to_s.scan(/\d+/).first.to_i).to_s + "\n" }
    let(:correct_result) { correct_result_part1 + correct_result_part2 + correct_result_part3 }
    it 'returns string type' do
      expect { user.to_s.to be_a(String) }
    end
    it 'returns string in correct format' do
      expect { puts creator.to_s }.to output(correct_result).to_stdout
    end
  end

  context '#games_to_s' do
    let(:creator) { game_creator }
    it 'returns type string type' do
      expect { user.games_to_s.to be_a(String) }
    end
  end

  context '#dob_valid?' do
    it 'returns true if date format is correct' do
      expect { user.dob_valid?('2010-01-01').to be_truthy }
    end
    it 'returns false if date format is incorrect' do
      expect { user.dob_valid?('200101').to be_falsey }
    end
    it 'returns false if elements of date are in wrong order' do
      expect { user.dob_valid?('01-2101-01').to be_falsey }
    end
  end

  context '#find_user' do
    let(:reader) { ToArray.new }
    let(:path) { 'lib/csv/users.csv' }
    let(:users) { reader.users_to_a(path) }
    let(:index) { user.find_user('3', users) }
    let(:index_wrong) { user.find_user('4', users) }
    let(:result_correct) { 3 }
    let(:result_wrong) { nil }
    it 'returns correct array index' do
      expect(index).to eq(result_correct)
    end
    it 'returns nil if not found' do
      expect(index_wrong).to eq(result_wrong)
    end
  end

  context '#add_user' do
    let(:reader) { ToArray.new }
    let(:path) { 'lib/csv/users.csv' }
    let(:tmp) { 'lib/csv/tmp.csv' }
    let(:users) { reader.users_to_a(path) }
    let(:test_user) { user_creator }
    let(:file) { double('file') }
    let(:line_part1) { test_user.login.to_s + ';' + test_user.password.to_s + ';' + test_user.date_of_birth.to_s }
    let(:line_part2) { ';' + test_user.token.to_s + ';' + test_user.id.to_s + "\n" }
    let(:line) { line_part1 + line_part2 }
    let(:print) { 'Data urodzenia niepoprawna' }
    let(:wrong_date) { 'wrong_date' }
    it 'correctly opens file and writes proper line' do
      expect(File).to receive(:open).with(path, 'a').and_yield(file)
      expect(file).to receive(:write).with(line)
      test_user.add_user(path)
    end
    it 'prints Data urodzenia jest nieprawidlowa when date_of_birth is wrong' do
      test_user.date_of_birth = wrong_date
      expect { test_user.add_user(path) }.to output(print).to_stdout
    end
  end

  context '#write_data' do
    let(:user) { user_creator }
    let(:path1) { 'path1.txt' }
    let(:path2) { 'path2.txt' }
    let(:word) { random_name }
    it 'raises error when initialized with wrong arguments' do
      expect { user.write_data(bad, things, can, happen) }.to raise_error(NameError)
    end
    it 'properly writes data in file located in path' do
      FakeFS do
        tmp = File.open(path1, 'w')
        File.open(path2, 'w').write(word)
        expect { user.write_data(tmp, path2) }.to_not raise_error
      end
    end
  end

  context '#delete_user' do
    let(:reader) { ToArray.new }
    let(:path) { 'lib/csv/users.csv' }
    let(:tmp) { 'lib/csv/tmp.csv' }
    let(:csv_path) { 'lib/csv' }
    let(:path_csv) { 'users.csv' }
    let(:tmp_csv) { 'tmp.csv' }
    let(:file) { double('file') }
    let(:size) { users.size }
    let(:users) { reader.users_to_a(path) }
    let(:user) { users[0] }
    let(:correct) { 0 }
    config = File.expand_path('../../lib/csv', __FILE__)
    FakeFS::FileSystem.clone(config)
    it 'expects read file to exist and write file not to exist' do
      expect(File).to exist(path)
      expect(File).not_to exist(tmp)
    end
    it 'it decreases amount of lines after deleting an user' do
      test_io = StringIO.new
      user.write_data(test_io, path)
      expect(test_io.string.lines.count).to eq(size - 1)
    end
    it 'returns 0 after deleting first user in array' do
      test_io = StringIO.new
      user.write_data(test_io, path)
      result = test_io.string.split(';')
      expect(result[4]).not_to eq(correct)
    end
    it 'properly creates tmp file' do
      FakeFS do
        expect { File.open("#{config}/#{tmp_csv}", 'w') }.to_not raise_error
      end
    end
    it 'properly moves tmp and path between each other' do
      FakeFS do
        File.open("#{config}/#{tmp_csv}", 'w')
        expect { FileUtils.mv("#{config}/#{tmp_csv}", "#{config}/#{path_csv}") }.to_not raise_error
      end
    end
    it 'properly closes file' do
      FakeFS do
        f = File.open("#{config}/#{tmp_csv}", 'w')
        expect { f.close }.to_not raise_error
      end
    end
  end
end
